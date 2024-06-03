const tblBlog = "blogs";
const tblCart = "cart";
let blogId = null;

getBlogTable();
getCartTable();

// Functions for blog management
function createBlog(title, author, content, quantity, price) {
    let lst = getBlogs();

    const requestModel = {
        id: uuidv4(),
        title: title,
        author: author,
        content: content,
        quantity: quantity,
        price: price,
        totalPrice: quantity * price  // Calculate total price
    };

    lst.push(requestModel);

    const jsonBlog = JSON.stringify(lst);
    localStorage.setItem(tblBlog, jsonBlog);

    successMessage("Saving Successful.");
    clearControls();
}

function getBlogs() {
    const blogs = localStorage.getItem(tblBlog);
    let lst = [];
    if (blogs !== null) {
        lst = JSON.parse(blogs);
    }
    return lst;
}

function getBlogTable() {
    const lst = getBlogs();
    let count = 0;
    let htmlRows = '';
    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>
                <button type="button" class="btn btn-warning" onclick="editBlog('${item.id}')">Edit</button>
                <button type="button" class="btn btn-danger" onclick="deleteBlog('${item.id}')">Delete</button>
            </td>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
            <td>${item.quantity}</td>
            <td>${item.price}</td>
            <td><button type="button" class="btn btn-primary" onclick="addToCart('${item.id}')">Add to Cart</button></td>
        </tr>
        `;
        htmlRows += htmlRow;
    });

    $('#tbody').html(htmlRows);
}

// Functions for cart management
function addToCart(id) {
    let blogs = getBlogs();
    let cart = getCart();

    const blog = blogs.find(b => b.id === id);
    if (!blog) {
        errorMessage("Blog not found.");
        return;
    }

    const cartItem = cart.find(c => c.id === id);
    if (cartItem) {
        errorMessage("Item already in cart.");
        return;
    }

    const totalPrice = blog.quantity * blog.price;  // Calculate total price for cart item
    cart.push({ ...blog, totalPrice });

    const jsonCart = JSON.stringify(cart);
    localStorage.setItem(tblCart, jsonCart);

    successMessage("Added to Cart.");
    getCartTable();
}

function getCart() {
    const cart = localStorage.getItem(tblCart);
    let lst = [];
    if (cart !== null) {
        lst = JSON.parse(cart);
    }
    return lst;
}

function getCartTable() {
    const lst = getCart();
    let count = 0;
    let htmlRows = '';
    let overallTotal = 0;  // Variable to store overall total

    lst.forEach(item => {
        const htmlRow = `
        <tr>
            <td>${++count}</td>
            <td>${item.title}</td>
            <td>${item.author}</td>
            <td>${item.content}</td>
            <td>${item.quantity}</td>
            <td>${item.price}</td>
            <td>${item.totalPrice}</td>  // Display total price for the item
            <td><button type="button" class="btn btn-danger" onclick="removeFromCart('${item.id}')">Remove</button></td>
        </tr>
        `;
        htmlRows += htmlRow;
        overallTotal += item.totalPrice;  // Calculate overall total
    });

    $('#cartBody').html(htmlRows);

    // Display overall total in a separate row at the end of the table
    const overallRow = `
        <tr>
            <td colspan="6"><strong>Overall Total:</strong></td>
            <td><strong>${overallTotal}</strong></td>
            <td></td>
        </tr>
    `;
    $('#cartBody').append(overallRow);
}

function removeFromCart(id) {
    let cart = getCart();
    const removedItem = cart.find(c => c.id === id);

    if (!removedItem) {
        errorMessage("Item not found in cart.");
        return;
    }

    cart = cart.filter(c => c.id !== id);

    const jsonCart = JSON.stringify(cart);
    localStorage.setItem(tblCart, jsonCart);

    successMessage("Removed from Cart.");
    getCartTable();
}

// Other utility functions
$('#btnSave').click(function () {
    const title = $('#txtTitle').val();
    const author = $('#txtAuthor').val();
    const content = $('#txtContent').val();
    const quantity = $('#txtQuantity').val();
    const price = $('#txtPrice').val();

    if (blogId === null) {
        createBlog(title, author, content, quantity, price);
    } else {
        updateBlog(blogId, title, author, content, quantity, price);
        blogId = null;
    }

    getBlogTable();
})

function uuidv4() {
    return "10000000-1000-4000-8000-100000000000".replace(/[018]/g, c =>
        (+c ^ crypto.getRandomValues(new Uint8Array(1))[0] & 15 >> +c / 4).toString(16)
    );
}

function successMessage(message) {
    alert(message);
}

function errorMessage(message) {
    alert(message);
}

function clearControls() {
    $('#txtTitle').val('');
    $('#txtAuthor').val('');
    $('#txtContent').val('');
    $('#txtQuantity').val('');
    $('#txtPrice').val('');
    $('#txtTitle').focus();
}
