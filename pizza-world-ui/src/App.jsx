import './App.css';
import CreatePizza from './components/CreatePizza';
import LoginPage from './components/LoginPage';
import RegisterPage from './components/RegisterPage';

export default function App() {
  return (
    <>
      <LoginPage />
      {/* <RegisterPage /> */}
      <CreatePizza />
    </>
  );
}
