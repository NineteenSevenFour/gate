# **Packages` migration**

Once in a while, packages must be upgraded. This must be done according to each packages compatibility matrix.

## Workspace

```bash
nx migrate @nrwl/workspace
```

## Runtime Packages

```bash
nx migrate @angular/animations
nx migrate @angular/cdk
nx migrate @angular/common
nx migrate @angular/compiler
nx migrate @angular/core
nx migrate @angular/forms
nx migrate @angular/material
nx migrate @angular/platform-browser
nx migrate @angular/platform-browser-dynamic
nx migrate @angular/router
```

## Dev packages

```bash
nx migrate @angular-devkit/build-angular
nx migrate @angular-eslint/eslint-plugin
nx migrate @angular-eslint/eslint-plugin-template
nx migrate @angular-eslint/template-parser
nx migrate @angular/cli
nx migrate @angular/compiler-cli
nx migrate @angular/language-service
```

## Complete migration

```bash
nx migrate --run-migrations
```

```text
Powered by
  _   _ _            _                  ______               _____                      
 | \ | (_)          | |                |  ____|             / ____|                     
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | |__ ___  _   _ _ _| (___   _____   _____ _ __  
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \|  __/ _ \| | | | '__\___ \ / _ \ \ / / _ \ '_ \ 
 | |\  | | | | |  __/ ||  __/  __/ | | | | | (_) | |_| | |  ____) |  __/\ V /  __/ | | |
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_|  \___/ \__,_|_| |_____/ \___| \_/ \___|_| |_|
```
