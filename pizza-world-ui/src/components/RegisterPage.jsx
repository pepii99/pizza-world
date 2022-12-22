import { useFormik } from 'formik';
import { registerSchema } from '../schemas/RegisterSchema';

const RegisterPage = () => {
  const { values, errors, touched, handleBlur, handleChange, handleSubmit } =
    useFormik({
      initialValues: {
        userName: '',
        password: '',
      },

      validationSchema: registerSchema,

      onSubmit: async (values) => {
        await fetch(import.meta.env.VITE_API_URL + 'api/identity/register', {
          method: 'POST',
          headers: {
            'Content-Type': 'application/json',
          },
          body: JSON.stringify(values),
        }).then(() => {
          console.log('Registering...');
          console.log(values);
        });
      },
    });

  return (
    <div className="min-h-screen bg-gray-100 text-gray-800 antialiased py-24 flex-col justify-center sm:px-2">
      <div className="relative py-3 sm:max-w-md sm:mx-auto">
        <div className="text-center mb-6">
          <span className="text-2xl font-light">Register</span>
        </div>

        <div className="mt-4 bg-white shadow-md rounded-lg">
          <div className="h-2 bg-indigo-600 rounded-t-md"></div>
          <div className="px-8 py-6">
            <form onSubmit={handleSubmit} autoComplete="off">
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
                onBlur={handleBlur}
                className={
                  errors.userName && touched.userName ? 'input--error' : 'input'
                }
              />
              {errors.userName && touched.userName && (
                <p className="register-input-error">{errors.userName}</p>
              )}
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
                onBlur={handleBlur}
                className={
                  errors.password && touched.password ? 'input--error' : 'input'
                }
              />
              {errors.password && touched.password && (
                <p className="register-input-error">{errors.password}</p>
              )}

              <button
                type="submit"
                className="border mt-4 w-full px-2 bg-indigo-600 text-gray-200 h-10 rounded-md hover:bg-indigo-700"
              >
                Register
              </button>
            </form>
          </div>
        </div>
        <div className="mt-5 text-center">
          <span className="">
            Already have an account?{' '}
            <button className="text-indigo-600">Log in here</button>
          </span>
        </div>
      </div>
    </div>
  );
};

export default RegisterPage;
