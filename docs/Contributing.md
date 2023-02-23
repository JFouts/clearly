# Contributing to Clearly .NET

One of the easiest ways to contribute is to participate in discussions on GitHub issues. You can also contribute by submitting pull requests with code changes.

## Submitting an Issue

Start a discussion for a bug or feature request on the [repository issue tracker](https://github.com/JFouts/domainmodeling/issues).

Before reporting a new issue, try to find an existing issue if one already exists. If it already exists, upvote (👍) it. Also, consider adding a comment with your unique scenarios and requirements related to that issue. Upvotes and clear details on the issue's impact help us prioritize the most important issues to be worked on sooner rather than later. If you can't find one, that's okay, we'd rather get a duplicate report than none.

## Other Discussions

You can also have an open discussion on our discord server. TODO link.

## Submitting a PR

We are always happy to see PRs from community members both for bug fixes as well as new features. To help you be successful we've put together a few simple rules to follow when you prepare to contribute to our codebase:

### Finding an issue to work on

If you are looking to contribute by making a change to the repository you can check out the issues tagged as [Help Wanted](https://github.com/JFouts/domainmodeling/labels/help%20wanted). These issues are hand picked for community support.

If you would like to make a contribution to an area not documented here, first open an issue with a description of the change you would like to make and the problem it solves so it can be discussed before a pull request is submitted.

### Before writing code

We encourage you to discuss the preferred design with the team first by using the issue as a discussion point with the team. This will help avoid designing a solution to the issue that does not align with the overarching goal of the project as a whole.

### Before submitting the pull request

Before submitting a pull request, make sure that it checks the following requirements:

* You find an existing issue with the "help-wanted" label or discuss with the team to agree on adding a new issue with that label
* You post a high-level description of how it will be implemented and receive a positive acknowledgement from the team before getting too committed to the approach or investing too much effort in implementing it.
* You add test coverage following existing patterns within the codebase
* Your code matches the existing syntax conventions within the codebase
* Your PR is small, focused, and avoids making unrelated changes

If your pull request contains any of the below, it's less likely to be merged.
* Changes that break backward compatibility
* Changes that are only wanted by one person/company. Changes need to benefit a large enough proportion of ASP.NET developers.
* Changes that add entirely new feature areas without prior agreement
* Changes that are mostly about refactoring existing code or code style
* Very large PRs that would take hours to review (remember, we're trying to help lots of people at once). For larger work areas, please discuss with us to find ways of breaking it down into smaller, incremental pieces that can go into separate PRs.

### During pull request review

A core contributor will review your pull request and provide feedback. To ensure that there is not a large backlog of inactive PRs, the pull request will be marked as stale after two weeks of no activity. After another four days, it will be closed.

## Tests
* Tests need to be provided for every bug/feature that is completed.
* Tests only need to be present for issues that need to be verified by QA (for example, not tasks)
* If there is a scenario that is far too hard to test there does not need to be a test for it.
    * "Too hard" is determined by the team as a whole.

## Feedback

Your pull request will now go through extensive checks by the subject matter experts on our team. Please be patient Update your pull request according to feedback until it is approved by one of our team members. After that, one of our team members may adjust the branch you merge into based on the expected release schedule.