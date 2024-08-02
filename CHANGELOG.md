# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [unreleased]

## [3.0.12] - 2024-08-02

### Changed
- Updates dependencies to newest versions.

## [3.0.11] - 2023-07-23

### Changed
- Timestamps of folders and files are now taken from the last commit they got changed on.

## [3.0.10] - 2023-07-17

### Changed
- Design of tables has been changed in the light layout.
- The navigation is now hidden in the root element.

## [3.0.9] - 2023-07-17

### Fixed
- Some new template files didn't get copied to the output folder.
- When using a different branch than the default branch, the correct branch is now checked out.

## [3.0.8] - 2023-07-16

### Added
- The `.meta` file now allows you to add a description to each entry in your directory, either file or sub-directory.
- Markdown extensions have been activated. Now tables, footnotes and more can be parsed.
- File sizes are now parsed from Git entries.

### Changed
- The Git implementation resorts to working copy for now. It's easier to work with.
- Awsm.css sadly hat to be removed. The original author completely disappeared. I re-implemented a basic css layout.
- A available `Readme.md` will be parsed as before, but won't show up on the directory list.

### Fixed
- Using the temp folder explicitly now fixes some issues related to permissions on unix-based systems.

## [3.0.7] - 2023-06-14

### Added
- The Git implementation now works on the repository itself, and does not need a working copy any longer

### Changed
- Timestamps of files and folders are now taken from the commits the files and folders got changed. This may not necessarily be the _real_ timestamps, but git doesn't record them anyways.

### Fixed
- The tests now run independently from the operating system (w/r to path delimiters)

## [3.0.6] - 2023-05-13

### Added
- `Readme.md` now contains more explanation to get `Radia` up and running.

### Changed
- Some changes under the hood now allow better caching.

## [3.0.5] - 2023-02-08

### Added
- `GitFileProvider` now supports fetching the content of the remote repository regularly.

### Changed
- Theme has been changed to be lighter in color.

## [3.0.4] - 2023-02-06

### Fixed
- Uses Forward-Headers, so that the application can access scheme information when used with a proxy

## [3.0.3] - 2023-02-06

### Fixed
- Fixes an issue related to case-insensitive file systems

## [3.0.2] - 2023-02-03

### Fixed
- Added missing MemoryCache dependency

## [3.0.1] - 2023-02-03

### Changed
- Changed appsettings.json

## [3.0.0] - 2023-02-02

### Added
- Initial layout.
- Local files can be served.
- Git repositories can now be tracked.
- Multiple file providers are supported.
- Markdown files will be rendered inline.
- Hidden files are respected on Linux.
- Majority of code is now under test.


[unreleased]: https://github.com/DanielGilbert/Radia/compare/v3.0.12...HEAD
[3.0.12]: https://github.com/DanielGilbert/Radia/compare/v3.0.11...v3.0.12
[3.0.11]: https://github.com/DanielGilbert/Radia/compare/v3.0.10...v3.0.11
[3.0.10]: https://github.com/DanielGilbert/Radia/compare/v3.0.9...v3.0.10
[3.0.9]: https://github.com/DanielGilbert/Radia/compare/v3.0.8...v3.0.9
[3.0.8]: https://github.com/DanielGilbert/Radia/compare/v3.0.7...v3.0.8
[3.0.7]: https://github.com/DanielGilbert/Radia/compare/v3.0.6...v3.0.7
[3.0.6]: https://github.com/DanielGilbert/Radia/compare/v3.0.5...v3.0.6
[3.0.5]: https://github.com/DanielGilbert/Radia/compare/v3.0.4...v3.0.5
[3.0.4]: https://github.com/DanielGilbert/Radia/compare/v3.0.3...v3.0.4
[3.0.3]: https://github.com/DanielGilbert/Radia/compare/v3.0.2...v3.0.3
[3.0.2]: https://github.com/DanielGilbert/Radia/compare/v3.0.1...v3.0.2
[3.0.1]: https://github.com/DanielGilbert/Radia/compare/v3.0.0...v3.0.1
[3.0.0]: https://github.com/DanielGilbert/Radia/releases/tag/v3.0.0