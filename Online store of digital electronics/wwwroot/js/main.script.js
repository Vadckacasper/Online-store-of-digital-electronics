const swiper = new Swiper('.main-slider', {
    loop: false,
    spaceBetween: 30,
    navigation: {
        nextEl: '.rightNav',
        prevEl: '.leftNav',
    },
    breakpoints: {
        320: {
            slidesPerView: 1,
        },
        480: {
            slidesPerView: 2,
        },
        768: {
            slidesPerView: 3,
        },
        1024: {
            slidesPerView: 4,
        },
        1440: {
            slidesPerView: 5,
        },
        1900: {
            slidesPerView: 6,
        }
    }
});