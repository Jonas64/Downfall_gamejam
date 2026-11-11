# Changelog


## [1.0.4] - 2026-08-05
### Fixed
- Fixed sprite count reports when a sprite is packed in multiple atlases.
- Fixed index out of bounds in sprite atlas report.
- Fixed page count display in sprite atlas report.
- Fixed selection in sprite atlas report.
- Fixed sample for sprites packed in multiple atlases.

## [1.0.3] - 2026-04-29
### Fixed
- Fixed sprite atlas variant is not reporting correctly in Sprite Atlas report. Sprite atlas variant will now show as a child node of the main atlas. (DANB-1065)
- Fixed sprites does not show which page they are in All Sprite Atlas report.
- Fixed report and sample when sprites are packed in multiple atlases. (UUM-140052)

## [1.0.2] - 2026-01-22
### Changed
- Update minimum Unity version.

## [1.0.1] - 2026-01-15
### Added
- Columns for all Sprite Atlas reports can be sorted.

### Fixed
- Fixed secondary texture in Sprite Atlas mistaken as atlas with more than 1 texture. (DANB-1210)

## [1.0.0] - 2025-09-25
### Fixed
- Fixed All Sprite Atlas report not showing correct total memory size. (DANB-1066)
- Fixed All Sprite Atlas reporting columns data not showing sensible data.
- Fixed redundant revert button on report settings.
- Fixed obsolete usage of API.

## [1.0.0-pre.1] - 2025-07-17
### Added
- New package that provides Sprite Atlas analyzing in Unity Editor.
