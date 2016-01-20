# blap
I started working on this project once I started to get familiar and comfortable with the Unity platform. At the time I named the project blap as it was the first name that came to mind, oh the regret but I'm sticking with it. Currently the engine contains the following features
- Debug console which can be used on device.
  - Easily add debug commands.
- View management system.
  - Includes layer system.
  - Transitions views in and out when a new view will be occupying a layer.
- Event dispatcher.
  - Easily create a global event dispatcher that uses C# events.
- File system service.
  - Read and write files easily.
  - Uses [TinyJSON](https://github.com/pbhogan/TinyJSON) to write objects to file and then generate them from file.
  - Easy to use API.
- Web downloader to retrieve files from the internet.
  - Able to retry multiple times if a request fails. Uses exponential backoff when retrying a failed request.
  - Built in timeout handling.
- Coroutine runner to allow non MonoBehaviour classes to run coroutines.

# TODO
- Update project to latest Unity 5.3 patch.
- Update Facebook service to use latest SDK.
- Create documentation for systems.
- Create command system similar to Javascript's Promises.
