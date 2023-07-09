# **NX micro-frontend setup guide**

[![Github CI](https://github.com/NineteenSevenFour/gate/actions/workflows/ci.yaml/badge.svg)](https://github.com/NineteenSevenFour/gate/actions/workflows/ci.yaml) [![github CD](https://github.com/NineteenSevenFour/gate/actions/workflows/cd.yaml/badge.svg)](https://github.com/NineteenSevenFour/gate/actions/workflows/cd.yaml) [![codecov](https://codecov.io/gh/NineteenSevenFour/gate/branch/main/graph/badge.svg?token=cXAu8BCw8d)](https://codecov.io/gh/NineteenSevenFour/gate) 
<!-- ALL-CONTRIBUTORS-BADGE:START - Do not remove or modify this section -->
[![All Contributors](https://img.shields.io/badge/all_contributors-1-orange.svg?style=flat-square)](#contributors-)
<!-- ALL-CONTRIBUTORS-BADGE:END -->

This guide will provides step by step to build a web micro-frontend application structured with NX workspace. There are two different templates. One for application and one for library. Your project will most likely be splitted into multiple workspaces that are managed by different team building up their own micro-frontend applications and libraries to interface with the host.

## **Setup environment**

Refers to the [Environment documentation](https://github.com/NineteenSevenFour/template-portal-app/blob/main/docs/01-ENVIRONMENT.md) to ensure you have the correct toolchain installed before carrying out this guide.

## **Setup Pre-commit validation**

While working alone or in a team, it is good practice to setup pre-commit validation of the code and commit message. This ensure that your project has constant buildable codebase and consistant commit. The [CGit Hooks](https://github.com/NineteenSevenFour/template-portal-app/blob/main/02-GITHOOK.md) guide will help you to set that up.

## **Micro frontend application**

The [Angular MFE guide](https://github.com/NineteenSevenFour/template-portal-app/blob/main/03-MFE.md) will help you bootstrap you own Angular MFE similar to this one.

## **DotNetCore WebApi**

The [nx dotnet guide](https://github.com/NineteenSevenFour/template-portal-app/blob/main/04-DOTNET.md) will help you bootstrap you own DotNetCore webapi.

## **Setup CI / CD**

Different team uses different process adapted for their needs, but typically, you will have some sort of `SDLC` that include `Brnaching` and `Pull request`. Following this workflow, you will most likely use also `Continuous integration` and in case you don´t, then you should !

The following guide will helps you setup a [CI / CD](https://github.com/NineteenSevenFour/template-portal-app/blob/main/05-CI-CD.md) pipeline with `Github actions`. Refers to github for more information if this doesn´t fit your needs.

## **Packages upgrade**

From time to time, you will need to update / upgrade your packages to a newer version due to security fixes, support changes or new features adoption. The [Packages migration](https://github.com/NineteenSevenFour/template-portal-app/blob/main/06-MIGRATION.md) guide provides exemples of commands to upgrade the workspace, runtime and dev packages.

## **Wants to learn mode ?**

While this guide is tailored for our needs, this is based on many good blog articles and tools documentation listed in a [Reading list](https://github.com/NineteenSevenFour/template-portal-app/blob/main/07-REFERENCES.md).

## Contributors
<!-- ALL-CONTRIBUTORS-LIST:START - Do not remove or modify this section -->
<!-- prettier-ignore-start -->
<!-- markdownlint-disable -->
<table>
  <tbody>
    <tr>
      <td align="center" valign="top" width="14.28%"><a href="https://www.linkedin.com/in/brunoduval/"><img src="https://avatars.githubusercontent.com/u/48152847?v=4?s=100" width="100px;" alt="Bruno DUVAL"/><br /><sub><b>Bruno DUVAL</b></sub></a><br /><a href="https://github.com/nineteensevenfour/gate/commits?author=datatunning" title="Code">💻</a> <a href="https://github.com/nineteensevenfour/gate/commits?author=datatunning" title="Documentation">📖</a> <a href="#projectManagement-datatunning" title="Project Management">📆</a> <a href="https://github.com/nineteensevenfour/gate/pulls?q=is%3Apr+reviewed-by%3Adatatunning" title="Reviewed Pull Requests">👀</a> <a href="https://github.com/nineteensevenfour/gate/commits?author=datatunning" title="Tests">⚠️</a> <a href="#translation-datatunning" title="Translation">🌍</a></td>
    </tr>
  </tbody>
  <tfoot>
    <tr>
      <td align="center" size="13px" colspan="7">
        <img src="https://raw.githubusercontent.com/all-contributors/all-contributors-cli/1b8533af435da9854653492b1327a23a4dbd0a10/assets/logo-small.svg">
          <a href="https://all-contributors.js.org/docs/en/bot/usage">Add your contributions</a>
        </img>
      </td>
    </tr>
  </tfoot>
</table>

<!-- markdownlint-restore -->
<!-- prettier-ignore-end -->

<!-- ALL-CONTRIBUTORS-LIST:END -->

## Powered by

```text
  _   _ _            _                   _____                      ______               
 | \ | (_)          | |                 / ____|                    |  ____|              
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | (___   _____   _____ _ __ | |__ ___  _   _ _ __ 
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \ \___ \ / _ \ \ / / _ \ '_ \|  __/ _ \| | | | '__|
 | |\  | | | | |  __/ ||  __/  __/ | | |____) |  __/\ V /  __/ | | | | | (_) | |_| | |   
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_____/ \___| \_/ \___|_| |_|_|  \___/ \__,_|_|
```
