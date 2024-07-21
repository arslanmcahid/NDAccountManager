import React from 'react';
import "/Users/arslan/Desktop/NDAccountManager/react-nda/src/styles/style.css";
function MainContent() {
  return (
    <main className="main-content">
      <section className="user-info">
        <h2>Kullanıcı Bilgileri</h2>
        {/* Kullanıcı bilgileri buraya gelecek */
            console.log("maincontent h2 araligi")
        }
      </section>
      <section className="data-section">
        <h2>Veriler</h2>
        {/* Backend'den gelen veriler buraya gelecek */}
      </section>
    </main>
  );
}
export default MainContent;