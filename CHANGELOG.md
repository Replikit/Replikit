# Change Log

All notable changes to this project will be documented in this file. See [versionize](https://github.com/saintedlama/versionize) for commit guidelines.

<a name="1.0.0-alpha.3"></a>
## [1.0.0-alpha.3](https://www.github.com/Replikit/Replikit/releases/tag/v1.0.0-alpha.3) (2022-8-15)

### Features

* Improve message creation API ([b10173e](https://www.github.com/Replikit/Replikit/commit/b10173e4c1a7fd6d343420b46fd32e6a674a59a2))
* Update routing and hosting infrastructure ([c5be042](https://www.github.com/Replikit/Replikit/commit/c5be04241d9f478fed8c69b299d6118a57502842))
* **Core:** Simplify routing, extract hosting into a separate package ([aedba22](https://www.github.com/Replikit/Replikit/commit/aedba2271136cfd408fd1ea82270fa61eb5ac2ec))

### Bug Fixes

* Fix controller pipeline ([4f63698](https://www.github.com/Replikit/Replikit/commit/4f6369895ea68b3cb0f32de0aa858ef4b72eabb9))
* Fix views ([68b9286](https://www.github.com/Replikit/Replikit/commit/68b928609735512f0e3a9260b61b60787814f67f))
* Update extensions ([36f4a14](https://www.github.com/Replikit/Replikit/commit/36f4a14ec7dfc68f287f1148343faef618106736))
* **Telegram:** Make update fetching fault-tolerant ([8361d5b](https://www.github.com/Replikit/Replikit/commit/8361d5b763cdd4e79428fd762c87322ac970a4fb))

<a name="1.0.0-alpha.2"></a>
## [1.0.0-alpha.2](https://www.github.com/Replikit/Replikit/releases/tag/v1.0.0-alpha.2) (2022-6-28)

### Features

* Redesign storage system ([af1d2da](https://www.github.com/Replikit/Replikit/commit/af1d2dad740638fa1ddd113ea32246413435a348))
* **EntityFrameworkCore:** Add EFCore integration ([35fd688](https://www.github.com/Replikit/Replikit/commit/35fd688b1b643fc5bb11c03a37d0a655ba59e366))
* **MongoDB:** Add MongoDB integration ([7a2e83a](https://www.github.com/Replikit/Replikit/commit/7a2e83ad6cc9329cb4e5e2a6e914c51c16f799d7))
* **Users:** Add users module ([4a3632a](https://www.github.com/Replikit/Replikit/commit/4a3632adfc5dcd47e164d57ae37eacbf80a76bc6))

### Bug Fixes

* Remove conflicting types ([3aa61d9](https://www.github.com/Replikit/Replikit/commit/3aa61d9a2eb15ecf326ba6a3e08bc2f02d3211bc))

<a name="1.0.0-alpha.1"></a>
## [1.0.0-alpha.1](https://www.github.com/Replikit/Replikit/releases/tag/v1.0.0-alpha.1) (2022-4-22)

<a name="1.0.0-alpha.0"></a>
## [1.0.0-alpha.0](https://www.github.com/Replikit/Replikit/releases/tag/v1.0.0-alpha.0) (2022-4-22)

### Features

* Redesign core abstractions ([b7884ab](https://www.github.com/Replikit/Replikit/commit/b7884ab393a04a5a0eedb9dc4c97e444957a0424))
* **Extensions:** Redesign extensions, unify state system ([f5d0ccd](https://www.github.com/Replikit/Replikit/commit/f5d0ccdd58568825e1fa91630933c8902e796baf))
* **Scenes:** Add Adapter property to scene context ([c4aee89](https://www.github.com/Replikit/Replikit/commit/c4aee89c2ed13f34b5509a311b13532db08678ba))
* **Telegram:** Update telegram adapter ([6dad2a2](https://www.github.com/Replikit/Replikit/commit/6dad2a2fcdbcc2d787766cb3f0475c8b28034bb9))
* **Views:** Add Adapter property to view context ([1f72734](https://www.github.com/Replikit/Replikit/commit/1f7273411aabd8f8f351f6a05d01a5c5d80dbdf4))

### Bug Fixes

* **Core:** Improve event handler system ([fb1c0e5](https://www.github.com/Replikit/Replikit/commit/fb1c0e5ae2293e6c0625cdd6b4c9331fdc86a3da))

<a name="0.5.0"></a>
## [0.5.0](https://www.github.com/Replikit/Replikit/releases/tag/v0.5.0) (2022-2-20)

### Bug Fixes

* Add missing common services ([77084b2](https://www.github.com/Replikit/Replikit/commit/77084b27fb96cc1f01cedae5bd2336c96e699ba2))
* Fix AddButtonRow ([5c8203e](https://www.github.com/Replikit/Replikit/commit/5c8203e47ffdcb772d1162b05a0e329b87cb20c1))
* Rename MemberCollectionFeatures to MemberServiceFeatures ([b91fe96](https://www.github.com/Replikit/Replikit/commit/b91fe96a2a8cfbd6b876fc2248ec566dcbca6901))
* **Core:** Fix Command pattern for optional parameters ([48f5a20](https://www.github.com/Replikit/Replikit/commit/48f5a2054ed592f11003d422173436baa77ef9c9))
* **Core:** Return int from IdentifierConverter where possible ([6d3aa2c](https://www.github.com/Replikit/Replikit/commit/6d3aa2ca971b6b4720342595daccd212c0348007))
* **Telegram:** Add title for direct channels ([a8ba8a8](https://www.github.com/Replikit/Replikit/commit/a8ba8a8a108b8ee7a4ca33bad73a24cddedcdba7))

### Features

* Add ability to answer to inline button pressing ([010a05d](https://www.github.com/Replikit/Replikit/commit/010a05d2bf3ee133f1c66fad29c2064e0719cc1b))
* Add channel service abstractions ([0501bd7](https://www.github.com/Replikit/Replikit/commit/0501bd7f74ac9d1104474deef4cfc678b8462753))
* **Core:** Add global member service ([130c787](https://www.github.com/Replikit/Replikit/commit/130c787a068720f4fa00ab9beca6264d5d7e697f))
* **Core:** Add more global services ([bdafa74](https://www.github.com/Replikit/Replikit/commit/bdafa74eee9d548b7ea2b9d9f3c620e397fc90ef))
* **Telegram:** Implement channel service ([2baed21](https://www.github.com/Replikit/Replikit/commit/2baed21841c4ddf9ef89727f426449c1658cc1cf))

<a name="0.4.0"></a>
## [0.4.0](https://www.github.com/Replikit/Replikit/releases/tag/v0.4.0) (2022-2-16)

### Bug Fixes

* Allow dependency injection in text parameter converters ([a1b023b](https://www.github.com/Replikit/Replikit/commit/a1b023b46f2d163a2888593de457d99c1cf35b25))

### Features

* Improve view manager api ([1717601](https://www.github.com/Replikit/Replikit/commit/171760190714379cc5b1d3a8e2ed9fe0dfd6c83a))
* **Telegram:** Add basic MemberService ([4e73a1a](https://www.github.com/Replikit/Replikit/commit/4e73a1a2e4593a5e77d52f11a931891e36a8880f))
* **Telegram:** Implement Pin and Unpin methods ([1ce9552](https://www.github.com/Replikit/Replikit/commit/1ce9552ac52a9df9c9c80fe175787562188e1453))

<a name="0.3.2"></a>
## [0.3.2](https://www.github.com/Replikit/Replikit/releases/tag/v0.3.2) (2022-1-9)

### Bug Fixes

* Update hosting infrastructure ([ee80a2a](https://www.github.com/Replikit/Replikit/commit/ee80a2a93741a5d9c0c7d23e228345db826882f2))

<a name="0.3.1"></a>
## [0.3.1](https://www.github.com/Replikit/Replikit/releases/tag/v0.3.1) (2022-1-8)

### Bug Fixes

* Change introspection transformer order ([84031e3](https://www.github.com/Replikit/Replikit/commit/84031e34458c3b1dc996d711466d1874cd2b63e6))
* Update Kantaiko.ConsoleFormatting ([0090b54](https://www.github.com/Replikit/Replikit/commit/0090b54a7865a6c3b7de0d19e6f03813ebe0b9da))
* **Core:** Exclude hidden parameters from command pattern ([b68a8f7](https://www.github.com/Replikit/Replikit/commit/b68a8f7ab815e39fbe5ae7de3f0c08667fcaceab))

<a name="0.3.0"></a>
## [0.3.0](https://www.github.com/Replikit/Replikit/releases/tag/v0.3.0) (2022-1-8)

### Bug Fixes

* Add Async suffixes ([68b390e](https://www.github.com/Replikit/Replikit/commit/68b390e7e526ff833f4dc1e37613f6665453b287))
* Add missing Async suffixes ([f9eb90a](https://www.github.com/Replikit/Replikit/commit/f9eb90ac1f4fef22996deb3a827071517cb8538f))
* **Core:** Remove the call of Regex.Escape on command prefixes because of extra call ([0584276](https://www.github.com/Replikit/Replikit/commit/05842765308b696a206cf686cac4bdfa23786b5b))
* **Core:** Rename MessageAdapterEventHandler to MessageEventHandler ([f3585dd](https://www.github.com/Replikit/Replikit/commit/f3585ddaf8093b790e3275a1cd38f2eb35b3237e))

### Features

* Move to new hosting and routing infrastructure ([bd790b5](https://www.github.com/Replikit/Replikit/commit/bd790b548a95d82affab62d906056730ce216eff))
* **Core:** Add AccountEventHandler ([f579674](https://www.github.com/Replikit/Replikit/commit/f579674a2bbe0b099c0484cf1c225d59fb47a048))
* **Core:** Add global services ([825d762](https://www.github.com/Replikit/Replikit/commit/825d762429701d4edffb47af7aa0e1b7ed30f571))
* **Core:** Add json converters for identifiers ([f3e14cb](https://www.github.com/Replikit/Replikit/commit/f3e14cbc747f9764a7c0b800e73312c6b5874c0b))

<a name="0.2.0"></a>
## [0.2.0](https://www.github.com/Replikit/Replikit/releases/tag/v0.2.0) (2021-11-17)

### Features

* Add templates ([a79dbf3](https://www.github.com/Replikit/Replikit/commit/a79dbf3ef4b9ce18354b4b6cb2fd5e01d41f56c3))
* **Core:** Expose MessageCollection implementation ([e69c3e2](https://www.github.com/Replikit/Replikit/commit/e69c3e209a40ecb8989f02b1d6fb6bda3724e61c))
* **Scenes:** Allow external scene activation ([c5257bd](https://www.github.com/Replikit/Replikit/commit/c5257bd17f3df52c8551633ca81d5577b9d6904c))

<a name="0.1.0"></a>
## [0.1.0](https://www.github.com/Replikit/Replikit/releases/tag/v0.1.0) (2021-11-9)

