document.addEventListener("DOMContentLoaded", function () {
    let images = document.querySelectorAll(".slider img");
    let indicatorsContainer = document.querySelector(".slider-indicators");
    let index = 0;

    // Tạo vòng tròn chỉ số dựa trên số lượng ảnh
    images.forEach((_, i) => {
        let span = document.createElement("span");
        if (i === 0) span.classList.add("active");
        span.dataset.index = i;
        indicatorsContainer.appendChild(span);
    });

    let indicators = document.querySelectorAll(".slider-indicators span");

    function showImage(i) {
        images[index].classList.remove("active");
        indicators[index].classList.remove("active");

        index = i;

        images[index].classList.add("active");
        indicators[index].classList.add("active");
    }

    function showNextImage() {
        let nextIndex = (index + 1) % images.length;
        showImage(nextIndex);
    }

    // Tự động chuyển ảnh sau 3 giây
    setInterval(showNextImage, 3000);

   
   
});
