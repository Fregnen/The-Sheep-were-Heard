# P8
Project on creating a room for the child and their teacher, where they can form a connection by interacting with musical instruments together, or the teacher can put on some music to which they can move/dance. 
# Table of Contents
1. [Instructions for Git](#insGit) <br />
	1. [First Time User](#first) <br />
	2. [Terms](#terms) <br />
	3. [Cloning a Repository](#clone) <br />
	4. [Working on a Project](#work) <br />
		1. [Git Bash](#bash) <br />
		2. [Get an Overview](#overview) <br />
		3. [Branches](#branches) <br />
		4. [Add, Commit, and Push](#push) <br />
		5. [Fetch and Pull](#pull) <br />
2. [Instructions for Unity](#insUnity) <br />

<a name="insGit"></a>
# Instructions for Git
This is an overview of how to use Git. In the [next section](#insUnity), you can also see how to name files and gameObjects to keep consistency, making it easier to navigate the Unity project.

<a name="first"></a>
## First Time User
If not already done, do this first:
* Make a [GitHub](https://github.com/) account
* Install [Git](https://git-scm.com/downloads) with Bash
	* Bash is Git's shell. You can use other shells, this is just the one I have learned how to use. 
	
<a name="terms"></a>
## Terms
There are 3 different staged:
* Locally (e.g. work in Unity), which is then...
* ... is committed (saved in local versions), which then can be...
* ... pushed to the remote version (online), which can be pulled by others. 

<a name="clone"></a>
## Cloning a Repository
When you want to clone a repository, you have to choose the folder where you want to save the repository. I recommend saving it on your SSD :bulb: 

**Example**
If you want to have the repository on <user>, go to the parent folder of <user>. Then, right-click the <user> folder and choose GIT Bash. A window should show up, where you write
	
```bash
git clone https://github.com/Fregnen/Semester-Project
```
You should now see the repository *Semester-Project* in <user> (or whatever folder you chose). Every time you clone a repository, install lfs:
	
```bash
git lfs install
``` 
This is telling Git that we are working with large files storage, such as Unity projects. 

<a name="work"></a>
## Working on a Project
:heavy_exclamation_mark:**This is where we all need to focus**:heavy_exclamation_mark:
To get a better understanding of how Git works, how branches, commits and merges are performed, here an [interactive tutorial](https://learngitbranching.js.org/) :information_source:

<a name="bash"></a>
### Git Bash
To open GIT Bash, open the folder (*Semester-Project*). Click inside the folder (just on the white/black area, where no files are shown) and choose GIT Bash.

<a name="overview"></a>
### Get an Overview
Here are different commands, which you can use to get an overview of the repository. Also see [Fetch and Pull](#pull). 

#### Git Status
When beginning your work *and* after every command, use 
```bash
git status
```
This gives a summary of your local status on the specific branch, you are working on. This is *not* compared with what is actually in the remote (online) version, but in comparison to the last time you have pushed or pulled.

#### Git Branch
    git branch                          list all the branches in the local machine
    git branch -a                       list all the branches (local + remote)
    git branch -av                      list all the branches (local + remote) with comments
	
<a name="branches"></a>
### Branches
Because we under **NO** circumstances want to make mistakes on *main*, we will for each feature you are working on, make a branch. These branches will be merged with *main*, when they are functioning as they should - but **only** by the Git Master. In this way we make sure that mistakes are only made on one branch and can be reverted, without it affecting the *main*. 
About local and remote branches: https://stackoverflow.com/questions/16408300/what-are-the-differences-between-local-branch-local-tracking-branch-remote-bra/24785777 

#### New Branch
Making a new branch and moving HEAD to it, type in Git Bash:
```bash
git checkout -b "BranchName"
```
Make sure that the [name makes sense](https://stackoverflow.com/questions/273695/what-are-some-examples-of-commonly-used-practices-for-naming-git-branches), when you create it. There are other methods to create a branch, which is seen in the tutorial previously linked. 
When having created and moved HEAD to the new branch, make sure to install lfs:
```bash
git lfs install
```
Now, check status to see that you are working on the branch with:
```bash
git status
```

#### Change Branch
Change the branch you are working on with 
```bash
git switch <BranchName>
```

#### Delete Branch
You cannot delete branches that you are working on, so checkout other branches first. Also, talk with the Git Master if in doubt of whether the branch should be merged with *main* before deleting - unless you have merged it with a branch you're already working on. 
```bash
git branch -d BranchName
```
The -d option will delete the branch only if it has already been pushed and merged with the remote branch. Use -D instead if you want to force the branch to be deleted, even if it hasn't been pushed or merged yet.

<a name="push"></a>
### Add, Commit, and Push
These are the steps for updating the local or remote repository.
#### Add
See status first, make sure you are working in the correct branch. This will also show if you have made any local changes. This will be shown by *red font*. These files are edited, but Git does not know if they are edited purposefully, hence showing you them. You now have to choose what files you want to add to your commit. You can either choose a specific file to add with:
```bash
git add <FileName>
```
or all files with
```bash
git add .
```
**Remember** to see git status, to make sure Git added the files you want. Now you can go on to commit.
If you want to remove a file, which you do not want to commit:
```bash
# For all files:
git restore .

# For specific file
git restore --staged <file name>
```

#### Commit
Here, you make a local save, which you can revert back to if something goes wrong later on. Please leave a message on what changes you have made to make it easier to follow the process but also to be able to remember what commit is working if you need to revert back to it. Use the command:
```bash
git commit -m "Your message here"
```
Again, get a git status, to make sure that what you just committed is the latest and that you are now working on this (have moved the HEAD with the commit).

If you find your work too messy to commit but still want to save the changes --> [read here](https://git-scm.com/book/en/v2/Git-Tools-Stashing-and-Cleaning).

#### Regretting a Commit
This command will remove the last commit from the current branch, but the file changes will stay in your working tree. Also the changes will stay on your index, so following with a git commit will create a commit with the exact same changes as the commit you "removed" before.
```bash
git reset --soft HEAD~1
```
[More information](https://stackoverflow.com/questions/3639115/reverting-to-a-specific-commit-based-on-commit-id-with-git) :information_source:

#### Push
When you want to share your progress on the branch you are working on, you can push the local to the remote repository. 
When creating something that everyone should have in their own version, you can make a [pull request](https://docs.github.com/en/free-pro-team@latest/github/collaborating-with-issues-and-pull-requests/about-pull-requests).

Again, always get an overview of where you are in the working tree (see [Get an Overview](#overview)). 
	
<a name="pull"></a>
### Fetch and Pull
To get an overview of what has happened remotely - also on other branches and where you are. 
```bash
git fetch
git status
```
If you want the changes, merge it with 
```bash
git merge origin/<otherbranch>
```
If you want to just get the updates, you can immediately pull with
```bash 
git pull
```
See the difference on fetch and pull here --> [:information_source:](https://stackoverflow.com/questions/292357/what-is-the-difference-between-git-pull-and-git-fetch)

<a name="insUnity"></a>
# Instructions for Unity :construction:
In Unity, we will work in different scenes, which makes it easier for the Git Master to distinguish the changes made by who.
<br /> [Examples of naming and commenting](http://environmentalcomputing.net/good-practice-for-writing-scripts/) <br />
[Naming conventions](https://www.c-sharpcorner.com/UploadFile/8a67c0/C-Sharp-coding-standards-and-naming-conventions/) <br />
[Naming conventions 2](https://www2.staffingindustry.com/eng/Editorial/Archived-Blog-Posts/Adam-Pode-s-Blog/Probably-the-best-file-naming-convention-ever) <br />
