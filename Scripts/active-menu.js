document.addEventListener("DOMContentLoaded", function () {

    
    let currentPath = window.location.pathname.toLowerCase();
    console.log("Current Path:", currentPath);

    let navLinks = document.querySelectorAll("#navMenu .nav-link");

    navLinks.forEach(link => {
        let linkPath = new URL(link.href, window.location.origin).pathname.toLowerCase();
    

        // So sánh bằng cách loại bỏ ".aspx" nếu có
        let normalizedCurrent = currentPath.replace(".aspx", "");
        let normalizedLink = linkPath.replace(".aspx", "");

        if (normalizedCurrent === normalizedLink) {
            link.classList.add("active");
      
        }
    });
});
