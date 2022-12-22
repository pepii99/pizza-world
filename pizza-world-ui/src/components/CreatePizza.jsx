import { useFormik } from 'formik';
import { CreatePizzaSchema } from '../schemas/CreatePizzaSchema';

const CreatePizza = () => {
  const { values, handleChange, handleSubmit } = useFormik({
    initialValues: {
      name: '',
      description: '',
      imageUrl: '',
    },
    validationSchema: CreatePizzaSchema,

    onSubmit: async (values) => {
      console.log('hello');
      let user = localStorage.getItem('token');
      console.log(user);

      var res = await fetch(import.meta.env.VITE_API_URL + 'api/pizza/create', {
        method: 'POST',
        headers: {
          Authorization: `Bearer ${user}`,
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(values),
      })
        .then((res) => res.json())
        .then((data) => {
          console.log(data);
        });
    },
  });

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800 antialiased py-24 flex-col justify-center sm:px-2">
      <div className="relative py-3 sm:max-w-md sm:mx-auto">
        <div className="text-center mb-6">
          <span className="text-2xl font-light">Create a new Pizza</span>
        </div>

        <div className="mt-4 bg-white shadow-md rounded-lg">
          <div className="h-2 bg-indigo-600 rounded-t-md"></div>
          <div className="px-8 py-6">
            <form onSubmit={handleSubmit}>
              <label htmlFor="username" className="black font-semibold">
                Name
              </label>
              <input
                id="name"
                required
                type="text"
                name="name"
                value={values.name}
                onChange={handleChange}
                className="border w-full h-5 mb-4 px-3 py-5 mt-2 hover:outline-none focus:outline-none focus:ring-1 focus:ring-indigo-400 rounded-md"
              />
              <label htmlFor="password" className="black font-semibold">
                Description
              </label>
              <textarea
                type="text"
                id="description"
                required
                name="description"
                value={values.description}
                onChange={handleChange}
                className="border mb-6 w-full px-3 py-2 mt-2 hover:outline-none focus:outline-none focus:ring-1 focus:ring-indigo-400 rounded-md"
              />
              <label htmlFor="password" className="black font-semibold">
                ImageUrl
              </label>
              <input
                type="text"
                id="imageUrl"
                required
                name="imageUrl"
                value={values.imageUrl}
                onChange={handleChange}
                className="border mb-6 w-full h-5 px-3 py-5 mt-2 hover:outline-none focus:outline-none focus:ring-1 focus:ring-indigo-400 rounded-md"
              />
              <button
                type="submit"
                className="border w-full px-2 bg-indigo-600 text-gray-200 h-10 rounded-md hover:bg-indigo-700"
              >
                Submit
              </button>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreatePizza;
