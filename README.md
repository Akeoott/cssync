# cssync

| c     | s       | sync |
|:-----:|:-------:|:----:|
| Cloud | Storage | Sync |

<div>
    <a>
        <img src="https://img.shields.io/github/last-commit/Akeoott/cssync?style=for-the-badge&logoSize=auto&labelColor=%23201a19&color=%23ffb4a2"/>
    </a>
    <a>
        <img src="https://img.shields.io/github/stars/Akeoott/cssync?style=for-the-badge&labelColor=%231d1b16&color=%23e6c419"/>
    </a>
    <a>
        <img src="https://img.shields.io/github/repo-size/Akeoott/cssync?style=for-the-badge&labelColor=%231a1b1f&color=%23a8c7ff"/>
    </a>
</div>

> [!WARNING]
> This project is WIP and POC

Written in C# for simplicity, performance and safety.<br>
Aiming for cross platform compatibility and support for various cloud storage services using rclone.

| cssync.Backend | cssync.Cli / cssync.Gui |
|:--------------:|:-----------------------:|
| Handles logic  | Handle user input       |

> [!NOTE]
> Planing to make backend fully independent from CLI and GUI,
> by just accepting args when running in a terminal.
> Gui is planned and will happen once the backend is finished.
