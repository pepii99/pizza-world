import { useFormik } from 'formik';
import { LoginSchema } from '../schemas/LoginSchema';

const LoginPage = () => {
  const { values, handleBlur, handleChange, handleSubmit } = useFormik({
    initialValues: {
      userName: '',
      password: '',
    },
    validationSchema: LoginSchema,

    onSubmit: async (values) => {
      var token = await fetch(
        import.meta.env.VITE_API_URL + 'api/identity/login',
        {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(values),
        }
      )
        .then((res) => res.json())
        .then((data) => {
          localStorage.setItem('token', data.token);
          console.log(values);
          console.log(data);
        });
    },
  });

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800 antialiased py-24 flex-col justify-center sm:px-2">
      <div className="relative py-3 sm:max-w-md sm:mx-auto">
        <div className="text-center mb-6">
          <span className="text-2xl font-light">Login to your account</span>
        </div>

        <div className="mt-4 bg-white shadow-md rounded-lg">
          <div className="h-2 bg-indigo-600 rounded-t-md"></div>
          <div className="px-8 py-6">
            <form onSubmit={handleSubmit}>
              <label htmlFor="username" className="black font-semibold">
                Username
              </label>
              <input
                id="username"
                required
                type="text"
                name="userName"
                value={values.userName}
                onChange={handleChange}
                className="border w-full h-5 mb-4 px-3 py-5 mt-2 hover:outline-none focus:outline-none focus:ring-1 focus:ring-indigo-400 rounded-md"
              />
              <label htmlFor="password" className="black font-semibold">
                Password
              </label>
              <input
                type="password"
                id="password"
                required
                name="password"
                value={values.password}
                onChange={handleChange}
                className="border mb-6 w-full h-5 px-3 py-5 mt-2 hover:outline-none focus:outline-none focus:ring-1 focus:ring-indigo-400 rounded-md"
              />
              <button
                type="submit"
                className="border w-full px-2 bg-indigo-600 text-gray-200 h-10 rounded-md hover:bg-indigo-700"
              >
                Log in
              </button>
            </form>
          </div>
        </div>
        <div className="mt-5 text-center">
          <span className="">
            Don't have an account?{' '}
            <button className="text-indigo-600">Register here</button>
          </span>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;
