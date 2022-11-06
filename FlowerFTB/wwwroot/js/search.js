var searchInput = document.getElementById("input-search")

searchInput.addEventListener('keyup',() => {
    var searchedProduct = searchInput.value;
    var productList = document.querySelector('#product-list')
    if (searchedProduct.length == 0) {
        productList.innerHTML = '';
    }
    else {
        fetch('/home/Search?searchText=' + searchedProduct)
            .then((response) => response.text())
            .then((data) => {
                productList.innerHTML = data
            });
    }


})