<!-- TOC -->

- [HtmlPartitionSync](#htmlpartitionsync)
    - [Introduce](#introduce)
    - [How to use](#how-to-use)
    - [example](#example)
    - [reference](#reference)

<!-- /TOC -->

# HtmlPartitionSync

## Introduce

- Dynamically generate javascript files so that you can html parts of your web pages and copy them with css to load them from your blog or other web pages
- You can synchronize parts of a specific page in real time when you are writing scripts that allow blogs, cafes, and so on.
- The basic idea is a lot of gist's javascript embed tag.
 
## How to use

1. Select source webpage url

    - `https://github.com/dotnet/core/blob/master/README.md`
    - For example, select the dotnet core README.md page.

2. Element selection
    - `//article`
    - Select the elements to synchronize to the xpath syntax.
    - For example, select `<article>` from the entire html document.

3. Generate a javascript-generating url by urlencoding the source webpage url and element xpath
    - `https://htmlpartitionsync.azurewebsites.net/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2Fdotnet%2Fcore%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle`

4. Insert a script tag into your target web page

    ```html
    <div>Here is the blog post area ...</div>
    <div><br /></div>
    <div>From the bottom, <a href="https://github.com/dotnet/core/blob/master/README.md">https://github.com/dotnet/core/blob/master/README.md</a> will only render part of the article.</div>
    <div>start ...</div>
    <div><br /></div>
    <div><br /></div>

    <script src="https://htmlpartitionsync.azurewebsites.net/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2Fdotnet%2Fcore%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle"></script>

    <div><br /></div>
    <div>finish ...</div>
    ```

## example
- https://hhd2002.blogspot.kr/2017/05/170518-htmlpartitionsync.html
 
## reference
- What is xpath syntax?
    - https://www.w3schools.com/xml/xpath_syntax.asp