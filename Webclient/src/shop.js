
import config from '../config.js';

const NODE_API_URL_PRODUCTSERVICES = config.NODE_API_URL_PRODUCTSERVICES;


async function fetchProducts() {
    try {
        const response = await fetch(`${NODE_API_URL_PRODUCTSERVICES}/Products`);
        if (!response.ok) {
            throw new Error(`HTTP error! status: ${response.status}`);
        }
        return await response.json();
    } catch (error) {
        console.error('Error fetching products:', error);
        return [];
    }
}

async function displayProducts() {
    const productList = document.getElementById('product-list');
    const products = await fetchProducts();

    products.forEach(async product => {
        const card = document.createElement('div');
        card.classList.add('bg-white', 'rounded-lg', 'shadow', 'overflow-hidden');

        const image = document.createElement('img');
        image.src =product.image;
        
        image.alt = product.name;
        image.classList.add('w-full', 'h-48', 'object-cover', 'mb-2');

        const textContainer = document.createElement('div');
        textContainer.classList.add('p-2');

        const name = document.createElement('h3');
        name.textContent = product.name;
        name.classList.add('font-bold', 'text-lg', 'mb-2');

        const description = document.createElement('p');
        description.textContent = product.description;
        description.classList.add('text-gray-700', 'mb-2');

        const price = document.createElement('p');
        price.textContent = `Price: ${product.price} kr`;
        price.classList.add('text-green-600', 'font-bold', 'mb-2');

        const addToCartButton = document.createElement('button');
        addToCartButton.textContent = 'Add to Cart';
        addToCartButton.classList.add('bg-green-500', 'text-white', 'py-2', 'px-4', 'rounded', 'hover:bg-green-700', 'cursor-pointer', 'mt-2');
        addToCartButton.addEventListener('click', () => addToCart(product));

        textContainer.appendChild(name);
        textContainer.appendChild(description);
        textContainer.appendChild(price);
        textContainer.appendChild(addToCartButton);

        card.appendChild(image);
        card.appendChild(textContainer);

        productList.appendChild(card);
    });
}

function addToCart(product) {
    let cart = JSON.parse(localStorage.getItem('cart')) || [];
    let found = cart.find(item => item.id === product.id);
    if (found) {
        found.quantity = (found.quantity || 1) + 1;
    } else {
        cart.push({ ...product, quantity: 1 });
    }
    localStorage.setItem('cart', JSON.stringify(cart));
    alert(`${product.name} added to cart`);
}


document.addEventListener('DOMContentLoaded', displayProducts);
