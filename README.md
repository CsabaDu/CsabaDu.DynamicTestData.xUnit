# CsabaDu.DynamicTestData.xUnit


🎯 An extension of CsabaDu.DynamicTestData framework to facilitate dynamic data-driven testing in xUnit.  

- ⚙️ Test data **conversion, management and provisioning**
- ⛑️ **Type-safe and thread-safe** support for *xUnit*
- 🧩 **Modular design**, abstractions and ready-to-use integrations
- 💼 **Portable** data sources
- 📋 Traceable **descriptive display names**
- 💵 **Now seeking sponsors** to complete v2.0 – comprehensive testing, documentation, examples, and new features!

---

[![Sponsor this project](https://img.shields.io/badge/Sponsor_on_GitHub-💖-ff69b4?style=flat-square)](https://github.com/sponsors/CsabaDu) 
[![Buy me a coffee](https://raw.githubusercontent.com/CsabaDu/CsabaDu.DynamicTestData/refs/heads/master/_Images/white-button_15.png)](https://buymeacoffee.com/csabadu) 
[![Support Me a Ko-fi](https://raw.githubusercontent.com/CsabaDu/CsabaDu.DynamicTestData/refs/heads/master/_Images/SupportMeOnKofi_20.png)](https://ko-fi.com/csabadu) 
[![OpenCollective](https://opencollective.com/static/images/opencollectivelogo-footer-n.svg)](https://opencollective.com/csabadudynamictestdata)  

---

## Documentation

This README and the [dedicated Wiki](https://github.com/CsabaDu/CsabaDu.DynamicTestData.xUnit/wiki) for the **CsabaDu.DynamicTestData.xUnit** extension are currently under development. While full documentation is in progress, you can already find essential information and usage guidance.

To learn more about the core framework, visit the [CsabaDu.DynamicTestData package on NuGet](https://www.nuget.org/packages/CsabaDu.DynamicTestData/2.0.6-beta) and explore its [main Wiki](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki).

For an overview of this xUnit-specific extension—including its purpose, integration points, and sample usage—see the [Extensibility & Ready-to-Use Implementations](https://github.com/CsabaDu/CsabaDu.DynamicTestData/wiki/02.08-%F0%9F%93%90-Extensibility-&-Ready-to-Use-Implementations#-xunit) section.

---

## Version 2.0.0-beta Foreword

The `CsabaDu.DynamicTestData.xUnit` framework has undergone a major transformation in version **2.0.0-beta**, introducing a wide range of enhancements while preserving its original foundation.

This release introduces powerful new capabilities:
- **Test data conversion** to any type of test data row
- **Data row management** for structured and reusable test inputs
- **Flexible data provisioning** to test methods across frameworks

These features make the framework easier to use, more adaptable to diverse testing needs, and better suited for integration with MSTest, xUnit, xUnit, and beyond. The newly introduced interfaces and abstract classes are designed for **extensibility**, allowing developers to support custom types and framework-specific features while staying aligned with the `CsabaDu.DynamicTestData` ecosystem.

The architecture is **clean**, the codebase is **modular**, and many features have been **partially tested**. The documentation provides detailed insights into the design, types, and usage patterns. However, this version is still considered **beta** due to:
- Incomplete test coverage
- Missing documentation sections (e.g., migration guide from v1.x.x)

**Final Notes**  

This version is beta, meaning:
  - Features are **stable but may change**  
  - Some features are only **partially tested**  
  - Documentation is detailed, but **incomplete**  
  - Feedback and support is **highly appreciated**  

---

## Changelog  

### **Version 2.0.0-beta** (2025-09-01)

> This is a **beta release** introducing **breaking changes**, new features, and architectural enhancements to the `CsabaDu.DynamicTestData.xUnit` library. These updates improve usability, flexibility, and extensibility.

- **Cancelled**:
  - **DynamicDataSources** namespace:
    - `DynamicTheoryDataSource` abstract class
- **Added**:
  - **DataRowHolders.Interfaces** namespace:
    - `ITheoryTestData` interface
    - `ITheoryTestData<TTestData>` interface
  - **DataRowHolders** namespace:
    - `TheoryTestData` abstract class
    - `TheoryTestData<TTestData>` sealed class
  - **DynamicDataSources** namespace:
    - `DynamicTheoryDataHolder` abstract class
    - `DynamicTheoryTestDataHolder` abstract class
  - **Attributes** namespace:
    - `MemberTestDataAttributeBase` abstract class
    - `MemberTestDataAttribute` sealed class

---
### **Version 1.0.0** (2025-03-20)

- Initial release of the `CsabaDu.DynamicTestData.xUnit` framework, which is a child of `CsabaDu.DynamicTestData` framework.
- Includes the `DynamicTheoryDataSource` base class.
- Provides support for dynamic data-driven tests with `TheoryData` arguments having different data, expected struct results, and exceptions, on top of the inherited `CsabaDu.DynamicTestData` features.

---
### **Version 1.1.0** (2025-04-01)

- **Added**: `AddOptional` method added to the `DynamicTheoryDataSource` class.
- **Note**: This update is backward-compatible with previous versions.

---
#### **Version 1.1.1** (2025-04-02)
- **Updated**:
  - README.md How it Works - Abstract `DynamicTheoryDataSource` Class section updated with `CheckedTheoryData` method explanation.
  - README.md Added explanation how `TheoryData` property and `ResetTheoryData` method work.
  - Small README.md corrections and visual refactorings.  

---
## Contributing

Contributions are welcome! Please submit a pull request or open an issue if you have any suggestions or bug reports.

---
## License

This project is licensed under the MIT License. See the [License](LICENSE.txt) file for details.

---
## Contact

For any questions or inquiries, please contact [CsabaDu](https://github.com/CsabaDu).

---
## FAQ

---
## Troubleshooting
