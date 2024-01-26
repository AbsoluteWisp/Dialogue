# Versioning philosophy and [Changelog](./CHANGELOG.md) format

## Flow
This format is very roughly based (more "inspired", really) on the ideas of [Keep a Changelog](https://keepachangelog.com/en/1.0.0/).  
The changelog is broken up into blocks.  
Every changelog block is a single version.  
Versions are named after the branches they're contained in and every branch corresponds to exactly one version.   
The more recent the version, the higher in the changelog file it is.

## `[EDGE]`
The top-most version, called `[EDGE]`, is special. It is the evolving block for the version that's currently being worked on. It contains the text "to be" before the version number to emphasize that it's not yet released. Change entries within `[EDGE]` can be marked as "ToDo" by adding a `^` sign before the change's type symbol.

## Block layout
Every block is an H2 (`##`) header.

Within every block, there is a brief description of the release, and one or more H3 (`###`) headers, each corresponding to a different change type within the release.  
The different change types are:
- `Added` `+` to denote newly added features
- `Changed` `/` to denote functionality changes to existing features
- `Fixed` `*` to denote bugfix changes to existing features
- `Deprecated` `~` to denote a feature to be removed in a future release
- `Removed` `-` to denote a feature that has just been removed

For consistency, every change is described either as an object ("New enumeration to describe something") or as a statement ("The program will now do A instead of B").  
To further increase legibility, every change starts with a symbol specific for its change type. Every change is written in plain text, with the starting symbol as inline code ("`~` Deprecate something")

## Example regular block
```md
## Logging - version 0.1.0 - released on 2023-12-17
The Logging release implements the infrastructure for elegant console logging in Dialogue.

### Added
`+ Logging namespace with all of the types introduced in this version`  
`+ Logger class to handle printing out text messages or exceptions on various levels`  
(and so on for other changes and change types)
```

## Example `[EDGE]` block
```md
## `[EDGE]` Logging - to be version 0.1.0
The Logging release implements the infrastructure for elegant console logging in Dialogue.

### Added
`+ Logging namespace with all of the types introduced in this version`  
`^+ Logger class to handle printing out text messages or exceptions on various levels`  
(and so on for other changes and change types)
```