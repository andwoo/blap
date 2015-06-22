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

  public class SingleViewLayer : AbstractLayer
  {
    private bool _transitionInProgress = false;
    private IView currentView = null;
    private Queue<QueuedView> viewQueue;

    public SingleViewLayer(GameObject container)
      : base(container)
    {
      viewQueue = new Queue<QueuedView>();
    }

    public override void PushView(ViewInfo info, object viewData)
    {
      if (!info.active)
      {
        viewQueue.Enqueue(new QueuedView(info, viewData));

        if (!_transitionInProgress)
        {
          _transitionInProgress = true;
          ShowNextViewInQueue();
        }
      }
    }

    public override void PopView(IView view)
    {
      if (currentView.viewInfo.id == view.viewInfo.id && currentView.viewInfo.active)
      {
        currentView.viewInfo.active = false;
        _transitionInProgress = true;
        TransitionOutView(currentView, delegate()
        {
          GameObject.Destroy(currentView.gameObject);
          currentView = null;
          ShowNextViewInQueue();
        });
      }
    }

    private void ShowNextViewInQueue()
    {
      if(viewQueue.Count > 0)
      {
        if (currentView != null)
        {
          //transition out the current view
          currentView.viewInfo.active = false;
          TransitionOutView(currentView, delegate()
          {
            GameObject.Destroy(currentView.gameObject);
            currentView = null;
            ShowNextViewInQueue();
          });
        }
        else
        {
          QueuedView qItem = viewQueue.Dequeue();
          currentView = InstantiatePrefabView(qItem.viewInfo, qItem.viewData);
          currentView.viewInfo.active = true;
          TransitionInView(currentView, delegate()
          {
            ShowNextViewInQueue();
          });
        }
      }
      else
      {
        _transitionInProgress = false;
      }
    }
  }
}