using blap.framework.webdownloader.events;
using blap.framework.webdownloader.interfaces;
using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;
using strange.extensions.command.impl;

namespace blap.framework.webdownloader.commands
{
  class DownloadTextureCommand : AbstractWebDownloadCommand
  {
    public override void Execute()
    {
      if (!string.IsNullOrEmpty((string)evt.data))
      {
        SendRequest<WebDownloadTextureResponse>(new WebDownloadRequest((string)evt.data), WebDownloadEvent.DOWNLOAD_TEXTURE_COMPLETE);
      }
    }
  }
}
