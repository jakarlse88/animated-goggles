window.linkToPageSection = function (elementId) {
    let element = document.querySelector(`${elementId}`);
    console.log(elementId);
    console.log(element);

    element.scrollIntoView();
};

window.initTyped = function () {
    const options = {
        strings: [
            ".NET Core",
            "Entity Framework Core",
            ".NET Core Identity",
            ".NET Core MVC",
            "SQL Server",
            "MS Server Management Studio",
            ".NET Core Web API",
            "React.js",
            ".NET Core Blazor",
            "JavaScript",
            "HTML5",
            "Bootstrap",
            "TailwindCSS",
            "CSS"
        ],
        shuffle: true,
        typeSpeed: 60,
        loop: true,
        loopCount: Infinity,
        showCursor: true,
        cursorChar: '|',
        smartBackspace: true,
        startDelay: 650,
        backSpeed: 60
    };

    const Typed = window.Typed;

    const typed = new Typed("#typed", options);
};
