# Unity-Examples

My portfolio of examples, modules and tools.
- **Core**: [AutumnYard/Scripts](Projects/AutumnYard%20Examples/Assets/_AutumnYard/Scripts)
- **Examples**: [Examples/Scripts](Projects/AutumnYard%20Examples/Assets/_Examples/Scripts)

---

## Modules

### Core/Patterns
**Singleton**: 
Four different implementations of the singleton pattern:
- *SingletonPOCO*: Just a simple C# object.
- *SingletonComponent*: A MonoBehaviour that can be used as component.
- *SingleInstance*: A MonoBehaviour without lazy initialization.
- *SingletonAsset*: A singleton that is instanced as an asset. Must be stored in the Resources folder.

```cs
abstract class SingletonPOCO<T> where T : new()
abstract class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour
abstract class SingletonAsset<T> : ScriptableObject where T : ScriptableObject
abstract class SingleInstance<T> : MonoBehaviour where T : MonoBehaviour
```

**Tasker**:
Implement and instantiate one or more *ITask* objects.
Instantiate the *Tasker*, provide a *StartTask* and then Enqueue your Tasks.
Call *Update*, *Enqueue* and *Insert* to manage.

**Updater**:
Instantiate the IUpdater interface, and then call Tools.Update with it.
### Core/Types
```cs
struct CounterFloat, struct CounterInt // Set a threshold and count.
struct CounterIntWithChance // Count up to the threshold, and then there's a chance to trigger.
struct Optional<T> // Make a type nullable, without making it nullable. 
```
Optional source is [aarthificial](https://gist.github.com/aarthificial/f2dbb58e4dbafd0a93713a380b9612af).

### Plugins/Addressables

**AssetLocators**
```cs
class ArrayAssetLocator<T> where T : UnityEngine.Object
class DictionaryAssetLocator<T> where T : UnityEngine.Object
// T is the Type of asset to load
// Dictionary and Array is the structure where they will be stored
```
1. First pass the AssetLabelReference to *Load*.
    - Check for exceptions: *NullReferenceException*: If the AssetLocator was null.
    - You can provide an Enum to check the validity of the Array version.
4. When async operations have finished, retrieve with self property (*this*).
5. Call *Unload* to Release Addressable references.

**Loaders**
```cs
class AssetReferenceLoader // C# class
class AssetReferenceLoaderComponent : MonoBehaviour // MonoBehaviour
// The MonoBehaviour is just a wrapper for the C# class.
```
1. Provide an *AssetLabelReference*.
    - Optional: Provide a *validateNumber* to check if the number of assets loaded was correct.
2. Call *Load*.
    - Check for exceptions: *AssetException* if operation failed, or if the *validateNumber* wasn't correct.
3. When async operations have finished, retrieve with self property (*this*).
4. Call *Unload* to Release Addressable references.

### Plugins/Odin Editor Windows

Depends on the paid plugin Sirenix Odin.
- **Window_Build**: A window to help make Builds easily and cleanly. Implements 'AutumnYard/Tools/Build'.
- **Window_DataAssets**: A window to manage the ScriptableObjects of the project.
- **Window_Tools**: A window to manage the whole game, all systems, data and state can be managed from here. Provides a unified interface for all the ScriptableObject singletons, indexes all the ScriptableObjects that contain data and configurations, and provides methods to set the state of the game however the user wants.

### Other plugins

- **CSVReader**: A wrapper for a downloaded CSVSerializer script. See *Sources and Disclaimers* for more info.
- **InputSystem**: Contains the autogenerated scripts for the InputActionMaps of the official Unity Input System.
- **SimpleJSON**: Base SimpleJSON, with a Unity add-on and some more methods of mine.
- **DOTween**
- **LeanTween**

### System specific plugins

- **NintendoSwitch**: Implements IO File operations specific to Windows.
- **SonyPlaystation4**: Implements IO File operations specific to Windows.
- **Steam**: *SteamManager* is the Singleton object that manages connections to Steam API.
- **Windows**: Implements IO File operations specific to Windows.

---

## Tools

- **Benchmarking**: Instantiate *BenchmarksHandler* and create Benchmarks within. Alternatively, implement your own *IBenchmark*. Provide the logging module where you want to output to.
- **Build**: *BuildTools* implements all the methods necessary to make a customized build with code. *BuildConfiguration* stores all the data necessary. 
	- This can be implemented in a window: AutumnYard/Plugins/OdinEditorWindow/Window_Build
	- It can also be implemented in CLI: AutumnYard/Tools/CLI/CLI_Build
- **CLI**: For now it only has *CLI_Build* that implements Tools/Build
- **Command**: Command design pattern implementation, inspired by [Infallible Code](https://youtu.be/f7X9gdUmhMY) and [Jason Weimann](https://youtu.be/UoNumkMTx-U)
- **IO**: Two modules
  - *File*: Provides all the methods necessary for Input/Output into files. This is a partial class, extended by the plugins for each system: Plugins/NintendoSwitch, Plugins/SonyPlaystation4, etc.
  - *IPersistence* and *IPersistenceChild*: The interfaces to implement save states. For a parent that saves itself, and a child that only passes forth its serialization.
- **Logging**: An *ILogger* that can be filtered with an Enum. I provide an example enum in *LogFilterEnum*.
- **Scene Management**. There are two parts:
  - **SceneLoaders**: Implement *ISceneLoader*. There are two implementations provided: *SceneInBuild* and *SceneInAddressables*. The first one can handle loading one or multiple scenes to load.
  - **Handlers**: Handle the SceneLoaders. Provided *DictOfScenesHandler*.
- **Screenshot Maker**: Wrapper for Unity's built-in module *ScreenCapture*
- **Terminal**: A command terminal taken from [stillwwater in github](https://github.com/stillwwater/command_terminal)


---

## Notes
- There's an intended codependency between Plugins/Scene and Addressables, because *SceneInAddressablesLoader* uses both. I decided to keep it in Plugins/Scene.

---

## Sources and Acknowledgements

Third Party Plugins:
- **DOTween**
- **LeanTween**

Official Unity plugins:
- **Addressables**
- **Input System**
- **Localization**

Official Unity built-in modules:
- **Screen Capture**

Other:
- **CSVSerializer**: I can't find the source anymore. I guess I downloaded it from the Unity Asset Store, but it was removed from over there. The link I found is https://assetstore.unity.com/packages/slug/135763, but it doesn't work anymore. Used in _Scripts/Plugins/CSVReader_.
- **Command** pattern implementation inspired by [Infallible Code](https://youtu.be/f7X9gdUmhMY) and [Jason Weimann](https://youtu.be/UoNumkMTx-U), who was inspired by Robert Nystrom's Game Patterns.
- **Command Terminal** implementation source is [stillwwater](https://github.com/stillwwater), [link to the github repository](https://github.com/stillwwater/command_terminal), he acknowledges he based it on Jonathan Blow's implementation in his own programming language. So thank you to the both of you!
