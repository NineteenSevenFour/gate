const appName = 'portal';
const appPath = `apps/${appName}`;
const rootPath = '../..';

module.exports = {
  extends: `${rootPath}/release.config.base.js`,
  name: appName,
  pkgRoot: `${rootPath}/dist/${appPath}`, // should come from angular.cli
  tagFormat: `${appName}-v${version}`,
  commitPaths: [
    `${rootPath}/package.json`,
    `${rootPath}/package-lock.json`,
    `*`, // anything in this directory
  ],
  assets: [`${appPath}/README.md`, `${appPath}/CHANGELOG.md`],
  plugins: [
    '@semantic-release/commit-analyzer',
    '@semantic-release/release-notes-generator',
    [
      '@semantic-release/changelog',
      {
        changelogFile: `${appPath}/CHANGELOG.md`,
      },
    ],
    [
      '@semantic-release/github',
      {
        assets: [
          {
            path: `${appPath}/**`,
            name: `${appName}-${nextRelease.gitTag}.zip`,
            label: `${appName}-(${nextRelease.gitTag})`,
          },
        ],
        successComment:
          "This ${issue.pull_request ? 'pull request' : 'issue'} is included in version ${nextRelease.version}",
        failComment:
          "This release from branch ${branch.name} had failed due to the following errors:\n- ${errors.map(err => err.message).join('\\n- ')}",
        releasedLabels: [
          'released<%= nextRelease.channel ? ` on @${nextRelease.channel}` : "" %> from <%= branch.name %>',
        ],
      },
    ],
    [
      '@semantic-release/git',
      {
        assets: ['package.json', 'package-lock.json', 'CHANGELOG.md'],
        message: `chore(release): ${appName}-v${nextRelease.version} [skip ci]\n\n${nextRelease.notes}`,
      },
    ],
  ],
};
