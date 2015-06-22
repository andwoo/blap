using System.Collections.Generic;
using UnityEngine;

namespace viewmanager
{
  class QueuedView
  {
    public ViewInfo viewInfo;
    public object viewData;

    public QueuedView(ViewInfo view, object data)
    {
      viewInfo = view;
      viewData = data;
    }
  }

  public class Layer
  {
    public GameObject viewcontainer { get; private set; }
    private Queue<QueuedView> viewQueue;
    private IView currentView;
    private bool _transitionInProgress;

    public Layer(GameObject container)
    {
      viewcontainer = container;
      viewQueue = new Queue<QueuedView>();
      _transitionInProgress = false;
    }

    public void PushView(ViewInfo view, object viewData)
    {
      viewQueue.Enqueue(new QueuedView(view, viewData));
      StartQueue();
    }

    private void StartQueue()
    {
      if (!_transitionInProgress)
      {
        _transitionInProgress = true;
        PushNextViewInQueue();
      }
    }

    private void PushNextViewInQueue()
    {
      if (viewQueue.Count > 0)
      {
        if (currentView != null)
        {
          TransitionOutCurrentView();
        }
        else
        {
          TransitionInView();
        }
      }
      else
      {
        _transitionInProgress = false;
      }
    }

    private void TransitionOutCurrentView()
    {
      currentView.TransitionOut(delegate()
      {
        GameObject.Destroy(currentView.gameObject);
        currentView = null;
        PushNextViewInQueue();
      });
    }

    private void TransitionInView()
    {
      QueuedView queue = viewQueue.Peek();
      viewQueue.Dequeue();
      currentView = LoadPrefabView(queue.viewInfo);
      queue.viewInfo.active = true;
      currentView.gameObject.transform.SetParent(viewcontainer.transform, false);
      currentView.SetViewInfo(queue.viewInfo, queue.viewData);
      currentView.TransitionIn(delegate()
      {
        PushNextViewInQueue();
      });
    }

    public void PopView(ViewInfo view)
    {
      if (currentView.viewInfo.id == view.id)
      {
        view.active = false;
        _transitionInProgress = true;
        TransitionOutCurrentView();
      }
    }

    private IView LoadPrefabView(ViewInfo view)
    {
      return (GameObject.Instantiate(Resources.Load(view.prefabPath), Vector3.zero, Quaternion.identity) as GameObject).GetComponent<IView>();
    }
  }
}
