# Core overview

This section will cover the core packages of the Replikit platform in details. If you are not already familiar with the
basic concepts, please start with the [Getting started](/docs/getting-started.md) guide.

The core infrastructure of the platform consists of two packages:

**Replikit.Abstractions**

It is the purest of all packages, defining the concept of adapters, their services and models, with which they connect
the client code and api of specific platforms.

[Read more](Replikit.Abstractions/README.md)

**Replikit.Core**

It is the minimum set of tools required by the client code. Defines the module system, mechanisms of controllers and
event handlers.

[Read more](Replikit.Core/README.md)
