document.addEventListener("DOMContentLoaded", function () {
    // Set a timeout to hide all divs with the class 'auto-hide' after 3 seconds
    setTimeout(function () {
        const elements = document.querySelectorAll(".alert"); // Select elements with the class
        elements.forEach(element => {
            element.style.display = "none"; // Hide each element
        });
    }, 3000); // 3000 milliseconds = 3 seconds
});