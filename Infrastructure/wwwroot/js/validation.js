
const formErrorHandler = (e, validationResult) => {

    let spanElement = document.querySelector(`[data-valmsg-for="${e.target.name}"]`)

    if (validationResult) {
        e.target.classList.remove('input-validation-error')
        e.target.classList.add('input-validation-valid')
        spanElement.classList.remove('input-validation-error')
        spanElement.classList.add('input-validation-valid')
        spanElement.innerHTML = '<i class="fa-solid fa-check"></i>'
    }
    else {
        e.target.classList.add('input-validation-error')
        e.target.classList.remove('input-validation-valid')
        spanElement.classList.add('input-validation-error')
        spanElement.classList.remove('input-validation-valid')
        spanElement.innerHTML = e.target.dataset.valRequired
    }
}

const lengthValidatorName = (value, minLength = 2) => {
    if (value.length >= minLength)
        return true

    else return false
}

const lengthValidatorPhone = (value, minLength = 7) => {
    if (value.length >= minLength)
        return true

    else return false
}

const compareValue = (value, compareValue) => {

    if (value === compareValue)
        return true;

    else return false;
}

const emailValidator = (value) => {
    const regex = /^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$/;
    if (regex.test(value))
        return true

    return false;
}

const passwordValidator = (value) => {
    const regex = /^(?=.*[A-Z])(?=.*\d)(?=.*[^A-Za-z0-9]).{8,}$/;
    if (regex.test(value))
        return true

    return false;    
}

const textValidation = (e) => {
    formErrorHandler(e, lengthValidatorName(e.target.value))
}
const phoneValidation = (e) => {
    formErrorHandler(e, lengthValidatorPhone(e.target.value))
}
const emailValidation = (e) => {
    formErrorHandler(e, emailValidator(e.target.value))
}
const passwordValidation = (e) => {
    formErrorHandler(e, passwordValidator(e.target.value))
}
const textAreaValidation = (e) => {
    formErrorHandler(e, lengthValidatorName(e.target.value))
}

let forms = document.querySelectorAll('form')
let inputs = forms[0].querySelectorAll('input, textarea')

inputs.forEach(input => {

    if (input.dataset.val === 'true') {

        console.log('point 1')


        input.addEventListener('blur', e => {

            switch (e.target.type) {

                case 'text':
                    textValidation(e)
                    break;
                case 'email':
                    emailValidation(e)
                    break;
                case 'tel':
                    phoneValidation(e)
                    break;
                case 'password':
                    passwordValidation(e)
                    break;
                case 'textarea':
                    textAreaValidation(e)
                    break;
            }
        })
    }
})


const accountDetailsForms = document.querySelectorAll('form[id*="account-details"]');

accountDetailsForms.forEach(form => {

    const inputs = form.querySelectorAll('input, textarea');

    inputs.forEach(input => {

        if (input.dataset.val === 'true') {

            input.addEventListener('blur', (e) => {

                switch (e.target.type) {
                    case 'text':
                        textValidation(e);
                        break;
                    case 'email':
                        emailValidation(e);
                        break;
                    case 'phonenumber':
                        phoneValidation(e);
                        break;
                    case 'password':
                        passwordValidation(e);
                        break;
                    case 'textarea':
                        textAreaValidation(e);
                        break;
                }
            });
        }
    });
});




const accountSecurityForms = document.querySelectorAll('form[id*="account-security"]');

accountSecurityForms.forEach(form => {

    const inputs = form.querySelectorAll('input, textarea');

    inputs.forEach(input => {

        if (input.dataset.val === 'true') {

            input.addEventListener('blur', (e) => {

                switch (e.target.type) {
                    case 'text':
                        textValidation(e);
                        break;
                    case 'email':
                        emailValidation(e);
                        break;
                    case 'phonenumber':
                        phoneValidation(e);
                        break;
                    case 'password':
                        passwordValidation(e);
                        break;
                    case 'textarea':
                        textAreaValidation(e);
                        break;
                }
            });
        }
    });
});