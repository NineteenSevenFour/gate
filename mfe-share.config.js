// Core libraries such as react, angular, redux, ngrx, etc. must be
// singletons. Otherwise the applications will not work together.
const coreLibraries = {
  '@angular/animations': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/cdk': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.9',
    eager: false,
  },
  '@angular/common': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/common/http': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/compiler': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/core': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/forms': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/material': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.9',
    eager: false,
  },
  '@angular/platform-browser': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/platform-browser-dynamic': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  '@angular/router': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.3.11',
    eager: false,
  },
  rxjs: {
    singleton: true,
    strictVersion: false,
    requiredVersion: '7.4.0',
    eager: false,
  },
  primeng: {
    singleton: true,
    strictVersion: false,
    requiredVersion: '13.4.1',
    eager: false,
  },
  primeicon: {
    singleton: true,
    strictVersion: false,
    requiredVersion: '5.0.0',
    eager: false,
  },
  primeflex: {
    singleton: true,
    strictVersion: false,
    requiredVersion: '3.2.1',
    eager: false,
  },
  '@ngneat/transloco': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '4.1.0',
    eager: false,
  },
  // workspace libraries
  '@portal/security': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '0.0.1',
    eager: false,
  },
  '@portal/navigation': {
    singleton: true,
    strictVersion: false,
    requiredVersion: '0.0.1',
    eager: false,
  },
};

module.exports = {
  // Share core libraries, and avoid everything else
  shared: (libraryName, defaultConfig) => {
    if (coreLibraries.hasOwnProperty(libraryName)) {
      return coreLibraries[libraryName] ?? defaultConfig;
    }

    // Returning false means the library is not shared.
    return false;
  },
};
