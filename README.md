# RSG Random String Generator
The new Random string generator that's backed up by a core library in .NET Core and using WPF as an interface for it.

This is a fun project in the meantime to inspire other work that comes along. 

# 2024 Update
Overtime, as a pet project, I want to modernize and minimize this API with a simple UI and maybe a CLI to integrate with other applications to generate random strings and words.
I will adopting most of my 2022 updates for this going forward.

## 2022 Update
I have revisted this project recently, and want to finish tidying up the core library before creating a nuget package.
- ✅ Update entire solution to .NET 6 ✅
- ✅ Update Randomization methods to be an abstraction ✅
- - This will add support for any random provider
- Support `CancellationToken` on all async API's (Large task)
- Generating statistics needs to be cleaned up
- Update in public API methods such as the Generate methods.
- - Support `CancellationToken` here mainly
- Use Request/Response for all Generate entry points 
- ✅ Remove SimpleIoc extensions.✅
- Update application configuration
- More multithreaded support for other complex actions like generating statistics report.
- Generalized multithread support for many complex, and computationally heavy work. 
- - This is becoming its own library, the [TaskRipper](https://www.github.com/Reapism/TaskRipper).

# Old project used as a base for features
[RSG-original repository](https://github.com/Reapism/RSG-original)

# Contributions
Contributions are welcome and are subject to some scrutiny.

In order to minimize as much scrutiny, sticking to these guidelines would help tremendously.
- Essentially, if a change is seen to benefit many people while keeping dependencies low, and not interfering with the core project ideals, the PR is likely to be accepted without much scrutiny.
