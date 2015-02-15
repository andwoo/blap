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
    private WebDownloadEvent _successEvent;

    protected void SendRequest<T>(WebDownloadRequest request, WebDownloadEvent successEvent) where T : AbstractWebDownloadResponse
    {
      this.Retain();
      _url = request.url;
      _successEvent = successEvent;
      service.downloadCompleteEvent += OnDownloadFinished;
      service.SendRequest<T>(request);
    }

    private void OnDownloadFinished(AbstractWebDownloadResponse response)
    {
      //move this to a base command?
      if (response.downloadPath == _url)
      {
        service.downloadCompleteEvent -= OnDownloadFinished;
        dispatcher.Dispatch(_successEvent, response);
        this.Release();
      }
    }
  }
}
