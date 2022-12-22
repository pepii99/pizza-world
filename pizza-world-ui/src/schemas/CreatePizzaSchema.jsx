import * as yup from 'yup';

export const CreatePizzaSchema = yup.object().shape({
  name: yup.string().min(5).required('Required'),
  description: yup.string().min(10).max(1000).required('Required'),
  imageUrl: yup.string(),
});
