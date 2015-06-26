using UnityEngine;

namespace viewmanager
{
  public abstract class AbstractLayer
  {
    public GameObject viewcontainer { get; private set; }

    public AbstractLayer(GameObject container)
    {
      viewcontainer = container;
    }

    public abstract void PushView(ViewInfo info, object viewData);

    public abstract void PopView(IView view);

    protected virtual void TransitionInView(IView view, TransitionComplete transitionComplete)
    {
      view.TransitionIn(delegate()
      {
        if (transitionComplete != null)
        {
          transitionComplete();
        }
      });
    }

    protected virtual void TransitionOutView(IView view, TransitionComplete transitionComplete)
    {
      view.TransitionOut(delegate()
      {
        if(transitionComplete != null)
        {
          transitionComplete();
        }
      });
    }

    protected IView InstantiatePrefabView(ViewInfo info, object viewData)
    {
      IView view = (GameObject.Instantiate(Resources.Load(info.prefabPath), Vector3.zero, Quaternion.identity) as GameObject).GetComponent<IView>();
      Object.DontDestroyOnLoad(view.gameObject);
      view.SetViewInfo(info, viewData);
      view.gameObject.transform.SetParent(viewcontainer.transform, false);
      return view;
    }
  }
}
