function loadCart() {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];    
    updateCartDisplay(cart);
  
}

function updateCartDisplay(cart) {
    const cartItemsList = document.getElementById('cart-items');
    cartItemsList.innerHTML = '';
    let total = 0;
    let cartTotal = document.getElementById('cart-total');
    cart.forEach((item, index) => {
        const cartItem = document.createElement('div');
        cartItem.classList.add('bg-white', 'rounded-lg', 'shadow', 'flex', 'items-center', 'p-4', 'mb-4');

        const image = document.createElement('img');
        image.src = item.image;
        image.alt = item.name;
        image.classList.add('w-24', 'h-24', 'object-cover', 'rounded-full', 'mr-4');

        const details = document.createElement('div');
        details.classList.add('flex', 'flex-grow', 'flex-col', 'justify-between');

        const name = document.createElement('p');
        name.textContent = item.name;
        name.classList.add('text-lg', 'font-bold', 'text-green-600');

        const price = document.createElement('p');
        price.textContent = `Price: ${item.price} kr`;
        price.classList.add('text-gray-600');
        const quantitySelector = document.createElement('input');
        quantitySelector.type = 'number';
        quantitySelector.value = item.quantity || 1;
        quantitySelector.min = 1;
        quantitySelector.classList.add('w-12', 'border', 'border-gray-300', 'rounded');
        quantitySelector.addEventListener('change', (event) => {
            updateItemQuantity(index, parseInt(event.target.value, 10));
        });
        const removeButton = document.createElement('button');
        removeButton.textContent = 'Remove';
        removeButton.classList.add('bg-red-500', 'text-white', 'py-2', 'px-4', 'rounded', 'hover:bg-red-600');
        removeButton.addEventListener('click', () => {
            removeFromCart(index);
        });

        details.appendChild(name);
        details.appendChild(price);
        details.appendChild(quantitySelector);

        cartItem.appendChild(image);
        cartItem.appendChild(details);
        cartItem.appendChild(removeButton);

        cartItemsList.appendChild(cartItem);
        total += item.price * (item.quantity || 1);
    });

    cartTotal.textContent = `Total: ${total} kr`;
}

function updateItemQuantity(index, newQuantity) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    if (cart[index]) {
        cart[index].quantity = newQuantity;
        localStorage.setItem('cart', JSON.stringify(cart));
        updateCartDisplay(cart);
    }
}

function removeFromCart(index) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    cart.splice(index, 1);
    localStorage.setItem('cart', JSON.stringify(cart));
    updateCartDisplay(cart);
}

document.addEventListener('DOMContentLoaded', loadCart);
