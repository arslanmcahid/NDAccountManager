import React from 'react';
import { useMsal, useAccount } from '@azure/msal-react';
import { useNavigate } from 'react-router-dom';
const Home = () => {
  const { instance } = useMsal();
  const account = useAccount(instance.getAllAccounts()[0]); // giris yapilan hesap
  const navigate = useNavigate();
  const handleLogout = () => {
    instance.logoutPopup()
      .then(() => {
        navigate('/login'); // after logout direction
      })
      .catch(error => {
        console.error("Logout error:", error);
      });
  };
  return (
    <div>
      <h1>Welcome to Home Page</h1>
      {account ? (
        <div>
          <p><strong>Username:</strong> {account.username}</p>
          <p><strong>Display Name:</strong> {account.name}</p>
          <p><strong>Id Token Claims:</strong></p>
          <p><strong>OID: </strong>{account.idTokenClaims.oid}</p>
          <pre>{JSON.stringify(account.idTokenClaims, null, 2)}</pre>
        </div>
      ) : (
        <p>No user info.</p>
      )}
      <button onClick={handleLogout}>Logout</button>
    </div>
  );
};
export default Home;