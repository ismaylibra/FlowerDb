const addToCartBtns = document.querySelectorAll(".add-to-cart");

function addToCart(ev) {
    console.log("working");
    const productId = ev.target.getAttribute("data-id");
    console.log(productId);
    fetch(`/basket/AddToBasket?productId=${productId}`).then(response => {
        console.log(response)
    })
}

addToCartBtns.forEach(addToCartBtn => addToCartBtn.addEventListener("click", addToCart))

