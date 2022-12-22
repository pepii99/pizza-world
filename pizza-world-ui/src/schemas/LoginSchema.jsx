import * as yup from 'yup';

export const LoginSchema = yup.object().shape({
  userName: yup.string().min(5).required('Required'),
  password: yup.string().min(4).required('Required'),
});
