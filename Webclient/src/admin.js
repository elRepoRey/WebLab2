import config from '../config.js';

const NODE_API_URL_PRODUCTSERVICES = config.NODE_API_URL_PRODUCTSERVICES;
const NODE_API_URL_FILESERVICES = config.NODE_API_URL_FILESERVICES;

// display all products
function getAllProducts() {
    fetch(`${NODE_API_URL_PRODUCTSERVICES}/Products`)
        .then(response => response.json())
        .then(data => {
            const productList = document.getElementById('product-list');
            productList.innerHTML = '';

            data.forEach(product => {
                const productCard = document.createElement('div');
                productCard.className = 'bg-white  rounded shadow-md rounded-lg overflow-hidden';
                const encodedProductData = encodeURIComponent(JSON.stringify(product));
                productCard.innerHTML = `
                    <img class="h-fit" src="${product.image}" alt="${product.name}">
                    <div class="px-4 pt-2 pb-4">
                    <h2 class="text-2xl font-bold mb-2">${product.name}</h2>
                    <p class="text-gray-700">${product.description}</p>
                    <p class="text-green-500 font-bold mt-2">${product.price} kr</p>
                    <div class="mt-4">
                        <button class="update-btn bg-blue-500 text-white py-2 px-4 rounded hover:bg-green-700"
                            data-product='${encodedProductData}'>
                            Update
                        </button>
                        <button class="delete-btn bg-red-500 text-white py-2 px-4 rounded hover:bg-red-700 ml-2">
                             Delete
                        </button>
                    </div>
                    </div>
                `;
                const deleteButton = productCard.querySelector('.delete-btn');
                deleteButton.addEventListener('click', function() {
                    deleteProduct(product.id);
                });
            
                productList.appendChild(productCard);
            });
            addUpdateEventListeners();
        })
        .catch(error => console.error('Error:', error));
}

function addUpdateEventListeners() {
    document.querySelectorAll('.update-btn').forEach(button => {
        button.addEventListener('click', function() {
            const encodedProductData = this.getAttribute('data-product');
            const productData = decodeURIComponent(encodedProductData);
            try {
                const product = JSON.parse(productData);
                toggleUpdateForm(product);
            } catch (e) {
                console.error('Error parsing product data:', e);
            }
        });
    });
}


//Add Product
function addProduct() {
    const containerName = 'images';
    const fileInput = document.getElementById('addfileInput');
    const formData = new FormData();
    formData.append('file', fileInput.files[0]);

    fetch(`${NODE_API_URL_FILESERVICES}/File?containerName=${containerName}`, {
        method: 'POST',
        body: formData
    })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => Promise.reject(error));
            }
            return response.json();
        })
        .then(data => {
            const productName = document.getElementById('addProductName').value;
            const productDescription = document.getElementById('addProductDescription').value;
            const productPrice = parseFloat(document.getElementById('addProductPrice').value);            
            const productImage = data.fileName;
            document.getElementById('addProductName').value = '';
            document.getElementById('addProductDescription').value = '';
            document.getElementById('addProductPrice').value = '';
            document.getElementById('addfileInput').value = '';

            const product = {
                Name: productName,
                Description: productDescription,
                Price: productPrice,
                Image: productImage
            };
            console.log(product);
            return fetch(`${NODE_API_URL_PRODUCTSERVICES}/Product`, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(product)
            });
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not OK');
            }
            console.log('Product added successfully');
            getAllProducts(); 
        })
        .catch(error => {
            console.error('Error:', error.message || error);
        });
}


function toggleUpdateForm(product) {
    const updateForm = document.getElementById('update-form-container');
    document.getElementById('updateProductId').value = product.id;
    document.getElementById('updateProductName').value = product.name;
    document.getElementById('updateProductDescription').value = product.description;
    document.getElementById('updateProductPrice').value = product.price;
    updateForm.classList.toggle('hidden');
    document.body.style.overflow = updateForm.classList.contains('hidden') ? 'auto' : 'hidden';
}


//Update Product
function updateProduct() {

    const productId = document.getElementById('updateProductId').value;
    const productName = document.getElementById('updateProductName').value;
    const productDescription = document.getElementById('updateProductDescription').value;
    const productPrice = parseFloat(document.getElementById('updateProductPrice').value);
    const fileInput = document.getElementById('updatefileInput');
    
    let updatePromise;

    if (fileInput.files.length > 0) {
        const formData = new FormData();
        formData.append('file', fileInput.files[0]);
        updatePromise = fetch(`${NODE_API_URL_FILESERVICES}/File?containerName=images`, {
            method: 'POST',
            body: formData
        })
        .then(response => {
            if (!response.ok) {
                return response.json().then(error => Promise.reject(error));
            }
            return response.json();
        })
        .then(data => {
            return {
                Image: data.fileName
            };
        });
    } else {
        updatePromise = Promise.resolve({
            Image: null
          
        });
        
    }

    updatePromise.then(fileData => {
        const product = {
            Id: productId,
            Name: productName,
            Description: productDescription,
            Price: productPrice,
            Image: fileData.Image
        };

        return fetch(`${NODE_API_URL_PRODUCTSERVICES}/Product/${productId}`, {
            method: 'PUT',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify(product)
        });
    })
    .then(response => {
        if (!response.ok) {
            throw new Error('Network response was not OK');
        }
        console.log('Product updated successfully');
        getAllProducts(); 
        closeUpdateForm();
    })
    .catch(error => {
        console.error('Error:', error.message || error);
    });
}

// delete a product
function deleteProduct(productId) {
    fetch(`${NODE_API_URL_PRODUCTSERVICES}/Product/${productId}`, {
        method: 'DELETE'
    })
    .then(response => response.json())
    .then(data => {
        console.log('Product deleted successfully:', data);
        getAllProducts();
    })
    .catch(error => {
        console.error('Error deleting product:', error);
    });
}


function closeUpdateForm() {
    const updateForm = document.getElementById('update-form-container');
    if (!updateForm.classList.contains('hidden')) {
        updateForm.classList.add('hidden');
        document.body.style.overflow = 'auto';

        const fileInput = document.getElementById('updatefileInput');
        fileInput.value = '';
    }
}

document.getElementById('addProductForm').addEventListener('submit', function (e) {
    e.preventDefault();
    addProduct();
});

document.getElementById('CancelUpdateProduct').addEventListener('click', function () {
    closeUpdateForm();
});

document.getElementById('addProductButton').addEventListener('click', function () {
    document.getElementById('addProductPanel').classList.toggle('hidden');
});

document.getElementById('updateProductForm').addEventListener('submit', function (e) {
    e.preventDefault();
    updateProduct();
});




document.getElementById('CancelAddProduct').addEventListener('click', function () {
    document.getElementById('addProductPanel').classList.toggle('hidden');});


getAllProducts();
