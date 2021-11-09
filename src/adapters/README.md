# Supported platforms

Replikit Framework provides several adapters for various platforms. Officially supported adapters are listed here.

## Feature table

This table shows the current status of support for different features by different adapters. Please note that some of
them may not be implemented at the moment, and some may not be available on a certain platform at all.

Legend:

- **\+** Fully supported
- **~** Partially supported
- **–** Completely unsupported due to platform limitations
- **?** Currently not implemented or investigated

| Feature \ Platform     | Telegram |
| ---------------------- | :------: |
| **MessageService**     |          |
| `Send`                 |    +     |
| `Edit`                 |    ~     |
| `Delete`               |    +     |
| `DeleteMany`           |    –     |
| `Get`                  |    –     |
| `GetMany`              |    –     |
| `Find`                 |    –     |
| `Pin`                  |    +     |
| `Unpin`                |    +     |
| **MemberService**      |          |
| `GetMany`              |    ?     |
| `ListMany`             |    ?     |
| `Add`                  |    ?     |
| `Remove`               |    ?     |
| `Ban`                  |    ?     |
| `Unban`                |    ?     |
| **Repository**         |          |
| `GetAccountInfo`       |    ~     |
| `GetChannelInfo`       |    +     |
| `ResolveAttachmentUrl` |    +     |
| **TextTokenizer**      |    +     |
| **TextFormatter**      |    +     |
| **EventSource**        |    +     |
