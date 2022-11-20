const appName = 'portal';
const appPath = `apps/${appName}`;
const artifactName = appName;
module.exports = {
  branches: [
    '+([0-9])?(.{+([0-9]),x}).x',
    'main',
    'next',
    'next-major',
    { name: 'beta', prerelease: true },
    { name: 'alpha', prerelease: true },
  ],
  name: appName,
  pkgRoot: `dist/${appPath}`, // should come from angular.cli
  tagFormat: artifactName + '-v${version}',
  commitPaths: ['force-release.md', `${appPath}/*`, 'libs/'], // should come from dep-graph and angular.json
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
            name: `${appName}` + '${nextRelease.gitTag}.zip',
            label: `${appName}` + ' (${nextRelease.gitTag})',
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
        message:
          `chore(release): ${artifactName}` +
          '-v${nextRelease.version} [skip ci]\n\n${nextRelease.notes}',
      },
    ],
  ],
};
