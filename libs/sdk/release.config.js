/* eslint-disable */

const libName = 'sdk';
const libPath = `libs/${libName}`;
const rootPath = '../..';

module.exports = {
  extends: `${rootPath}/release.config.base.js`,
  name: appName,
  pkgRoot: `${rootPath}/dist/${libPath}`, // should come from angular.cli
  tagFormat: `${libName}-v${version}`,
  commitPaths: [
    `${rootPath}/package.json`,
    `${rootPath}/package-lock.json`,
    `*`, // anything in this directory
  ],
  assets: [`${libPath}/README.md`, `${libPath}/CHANGELOG.md`],
  plugins: [
    '@semantic-release/commit-analyzer',
    '@semantic-release/release-notes-generator',
    [
      '@semantic-release/changelog',
      {
        changelogFile: `${libPath}/CHANGELOG.md`,
      },
    ],
    '@semantic-release/npm',
    [
      '@semantic-release/git',
      {
        assets: ['package.json', 'package-lock.json', 'CHANGELOG.md'],
        message: `chore(release): ${libName}-v${nextRelease.version} [skip ci]\n\n${nextRelease.notes}`,
      },
    ],
  ],
};
