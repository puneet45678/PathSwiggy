const apiBaseUrl = 'https://localhost:7171/api/customer';

document.addEventListener('DOMContentLoaded', () => {
    loadCustomers();
    document.getElementById('customerForm').addEventListener('submit', submitCustomerForm);
});

async function loadCustomers() {
    try {
        const response = await fetch(apiBaseUrl);
        const customers = await response.json();
        const customerBody = document.getElementById('customer-body');
        customerBody.innerHTML = '';

        customers.forEach(customer => {
            const row = document.createElement('tr');
            row.innerHTML = `
                <td>${customer.firstName}</td>
                <td>${customer.lastName}</td>
                <td>${customer.email}</td>
                <td>${customer.phoneNumber}</td>
                <td>${customer.loyaltyPoints}</td>
                <td>
                    <button class="btn-edit" onclick="editCustomer('${customer.id}')">Edit</button>
                    <button class="btn-delete" onclick="deleteCustomer('${customer.id}')">Delete</button>
                </td>
            `;
            customerBody.appendChild(row);
        });
    } catch (error) {
        console.error('Error loading customers:', error);
    }
}

function showCustomerForm(isEdit = false) {
    document.getElementById('customer-form').style.display = 'block';
    
    if (isEdit) {
        document.getElementById('form-title').innerText = 'Edit Customer';
    } else {
        document.getElementById('form-title').innerText = 'Add New Customer';
        document.getElementById('customerForm').reset(); 
        document.getElementById('customerId').value = '';
    }
}

function hideCustomerForm() {
    document.getElementById('customer-form').style.display = 'none';
}

async function submitCustomerForm(event) {
    event.preventDefault();
    const customerId = document.getElementById('customerId').value;

    const customerData = {
        firstName: document.getElementById('firstName').value,
        lastName: document.getElementById('lastName').value,
        email: document.getElementById('email').value,
        phoneNumber: document.getElementById('phone').value,
        address: document.getElementById('address').value,
    };

    try {
        if (customerId) {
            await fetch(`${apiBaseUrl}/${customerId}`, {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(customerData),
            });
        } else {
            await fetch(apiBaseUrl, {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(customerData),
            });
        }

        hideCustomerForm();
        loadCustomers();
    } catch (error) {
        console.error('Error saving customer:', error);
    }
}

async function editCustomer(customerId) {
    try {
        const response = await fetch(`${apiBaseUrl}/${customerId}`);
        const customer = await response.json();

        document.getElementById('customerId').value = customer.id;
        document.getElementById('firstName').value = customer.firstName;
        document.getElementById('lastName').value = customer.lastName;
        document.getElementById('email').value = customer.email;
        document.getElementById('phone').value = customer.phoneNumber;
        document.getElementById('address').value = customer.address;

        showCustomerForm(true);
        console.log(document.getElementById('customerId').value);
        console.log(document.getElementById('firstName').value);
        console.log(document.getElementById('phone').value);
        document.getElementById('form-title').innerText = 'Edit Customer';
    } catch (error) {
        console.error('Error editing customer:', error);
    }
}

async function deleteCustomer(customerId) {
    if (confirm('Are you sure you want to delete this customer?')) {
        try {
            await fetch(`${apiBaseUrl}/${customerId}`, { method: 'DELETE' });
            loadCustomers();
        } catch (error) {
            console.error('Error deleting customer:', error);
        }
    }
}
