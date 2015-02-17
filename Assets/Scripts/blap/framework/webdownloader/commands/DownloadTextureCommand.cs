﻿using blap.framework.webdownloader.events;
using blap.framework.webdownloader.requests;
using blap.framework.webdownloader.responses;

namespace blap.framework.webdownloader.commands
{
  class DownloadTextureCommand : AbstractWebDownloadCommand
  {
    public override void Execute()
    {
      if (!string.IsNullOrEmpty((string)evt.data))
      {
        SendRequest<WebDownloadTextureResponse>(new WebDownloadRequest((string)evt.data, 5f, 3, true), WebDownloadEvent.DOWNLOAD_TEXTURE_COMPLETE);
      }
    }
  }
}
