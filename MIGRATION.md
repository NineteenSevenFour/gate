
# **Packages` migration**

Once in a while, packages must be upgraded. This must be done according to each packages compatibility matrix.

## Workspace

nx migrate @nrwl/workspace@14.1.9

## Runtime Packages

nx migrate @angular/animations@13.3.11
nx migrate @angular/cdk@13.3.9
nx migrate @angular/common@13.3.11
nx migrate @angular/compiler@13.3.11
nx migrate @angular/core@13.3.11
nx migrate @angular/forms@13.3.11
nx migrate @angular/material@13.3.9
nx migrate @angular/platform-browser@13.3.11
nx migrate @angular/platform-browser-dynamic@13.3.11
nx migrate @angular/router@13.3.11

## Dev packages

nx migrate @angular-devkit/build-angular@13.3.9
nx migrate @angular-eslint/eslint-plugin@13.5.0
nx migrate @angular-eslint/eslint-plugin-template@13.5.0
nx migrate @angular-eslint/template-parser@13.5.0
nx migrate @angular/cli@13.3.9
nx migrate @angular/compiler-cli@13.3.11
nx migrate @angular/language-service@13.3.11

## Complete migration

nx migrate --run-migrations

# Powered by

``` 
Powered by
  _   _ _            _                  ______               _____                      
 | \ | (_)          | |                |  ____|             / ____|                     
 |  \| |_ _ __   ___| |_ ___  ___ _ __ | |__ ___  _   _ _ _| (___   _____   _____ _ __  
 | . ` | | '_ \ / _ \ __/ _ \/ _ \ '_ \|  __/ _ \| | | | '__\___ \ / _ \ \ / / _ \ '_ \ 
 | |\  | | | | |  __/ ||  __/  __/ | | | | | (_) | |_| | |  ____) |  __/\ V /  __/ | | |
 |_| \_|_|_| |_|\___|\__\___|\___|_| |_|_|  \___/ \__,_|_| |_____/ \___| \_/ \___|_| |_|
```
