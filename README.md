<!-- TOC -->

- [HtmlPartitionSync](#htmlpartitionsync)
    - [소개](#소개)
    - [사용방법](#사용방법)
    - [예제](#예제)
    - [레퍼런스](#레퍼런스)

<!-- /TOC -->

# HtmlPartitionSync

## 소개

- 웹페이지의 일부분을 html렌더링 해서 css와 함께 복사해서 블로그나 다른 웹페이지에서 로드할 수 있도록 동적으로 javascript 파일을 생성해 줌
- 블로그, 카페 등 스크립트가 허용된 글쓰기를 할때 특정페이지의 일부를 실시간으로 동기화 할 수 있음.
- 기본적인 아이디어는 gist의 javascript embed tag를 많이 참고 했음.

## 사용방법

1. 소스 웹페이지 url 선정
    - `https://github.com/dotnet/core/blob/master/README.md`
    - 예를들어 dotnet core의 README.md 페이지를 선택한다.

2. 엘리먼트 선택
    - `//article`
    - 동기화할 엘리먼트를 xpath 문법에 맞게 선택한다.
    - 예를들어 전체 html 문서중 `<article>` 을 선택한다.

3. 소스 웹페이지 url과 엘리먼트 xpath 를 urlencode 하여 javascript 생성 url 을 생성
    - `https://htmlpartitionsync.azurewebsites.net/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2Fdotnet%2Fcore%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle`

4. 타겟 웹페이지에 script 태그를 삽입

    ```html
    <div>여기는 블로그 글 영역 ...</div>
    <div><br /></div>
    <div>아래부터는 <a href="https://github.com/dotnet/core/blob/master/README.md">https://github.com/dotnet/core/blob/master/README.md</a> 에서 article 부분만 렌더링 함.</div>
    <div>시작 ...</div>
    <div><br /></div>
    <div><br /></div>

    <script src="https://htmlpartitionsync.azurewebsites.net/api/PartitionJs?url=https%3A%2F%2Fgithub.com%2Fdotnet%2Fcore%2Fblob%2Fmaster%2FREADME.md&xpath=%252F%252Farticle"></script>

    <div><br /></div>
    <div>끝 ...</div>
    ```

## 예제
- https://hhd2002.blogspot.kr/2017/05/170518-htmlpartitionsync.html

## 레퍼런스
- xpath 문법 이란?
    - https://www.w3schools.com/xml/xpath_syntax.asp