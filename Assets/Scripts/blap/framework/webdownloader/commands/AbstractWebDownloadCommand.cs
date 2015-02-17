using blap.framework.debug.utils;
using blap.framework.webdownloader.events;
using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;
using strange.extensions.command.impl;

namespace blap.framework.webdownloader.commands
{
  abstract class AbstractWebDownloadCommand : EventCommand
  {
    [Inject]
    public IWebDownloadService service { get; set; }

    private string _url;
    private WebDownloadEvent _completeEvent;

    protected void SendRequest<T>(WebDownloadRequest request, WebDownloadEvent completeEvent) where T : AbstractWebDownloadResponse
    {
      this.Retain();
      _url = request.url;
      _completeEvent = completeEvent;
      service.downloadCompleteEvent += OnDownloadFinished;
      service.SendRequest<T>(request);
    }

    private void OnDownloadFinished(AbstractWebDownloadResponse response)
    {
      if (response.url == _url)
      {
        service.downloadCompleteEvent -= OnDownloadFinished;
        dispatcher.Dispatch(_completeEvent, response);
        this.Release();
      }
    }
  }
}
