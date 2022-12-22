import * as yup from 'yup';

// const passwordRules = new RegExp(
//   '^(?=.*d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$'
// );

export const registerSchema = yup.object().shape({
  userName: yup.string().min(5).required('Required'),
  password: yup
    .string()
    .min(4)
    // .matches(passwordRules, { message: 'Please create a stronger password...' })
    .required('Required'),
});
