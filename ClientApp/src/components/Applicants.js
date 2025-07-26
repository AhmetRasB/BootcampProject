import React, { useState, useEffect } from 'react';
import { getApplicants, createApplicant, deleteApplicant } from '../services/api';

function Applicants() {
  const [applicants, setApplicants] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    nationalityIdentity: '',
    email: '',
    password: '',
    about: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const response = await getApplicants();
      setApplicants(response.data);
    } catch (error) {
      console.error('Error loading applicants:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createApplicant(formData);
      setFormData({
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        nationalityIdentity: '',
        email: '',
        password: '',
        about: ''
      });
      setShowForm(false);
      loadData();
    } catch (error) {
      alert('Error creating applicant: ' + error.message);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this applicant?')) {
      try {
        await deleteApplicant(id);
        loadData();
      } catch (error) {
        alert('Error deleting applicant: ' + error.message);
      }
    }
  };

  if (loading) {
    return <div className="card">Loading...</div>;
  }

  return (
    <div>
      <div className="card">
        <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
          <h2>Applicants</h2>
          <button className="btn" onClick={() => setShowForm(!showForm)}>
            {showForm ? 'Cancel' : 'Add Applicant'}
          </button>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} style={{ marginTop: '20px' }}>
            <div className="form-group">
              <label>First Name:</label>
              <input
                type="text"
                value={formData.firstName}
                onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>Last Name:</label>
              <input
                type="text"
                value={formData.lastName}
                onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>Date of Birth:</label>
              <input
                type="date"
                value={formData.dateOfBirth}
                onChange={(e) => setFormData({ ...formData, dateOfBirth: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>Nationality Identity:</label>
              <input
                type="text"
                value={formData.nationalityIdentity}
                onChange={(e) => setFormData({ ...formData, nationalityIdentity: e.target.value })}
                required
                maxLength="11"
              />
            </div>
            <div className="form-group">
              <label>Email:</label>
              <input
                type="email"
                value={formData.email}
                onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>Password:</label>
              <input
                type="password"
                value={formData.password}
                onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>About:</label>
              <textarea
                value={formData.about}
                onChange={(e) => setFormData({ ...formData, about: e.target.value })}
                rows="3"
              />
            </div>
            <button type="submit" className="btn btn-success">Create Applicant</button>
          </form>
        )}

        <table className="table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Date of Birth</th>
              <th>About</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {applicants.map(applicant => (
              <tr key={applicant.id}>
                <td>{applicant.firstName} {applicant.lastName}</td>
                <td>{applicant.email}</td>
                <td>{new Date(applicant.dateOfBirth).toLocaleDateString()}</td>
                <td>{applicant.about || '-'}</td>
                <td>
                  <button 
                    className="btn btn-danger" 
                    onClick={() => handleDelete(applicant.id)}
                  >
                    Delete
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default Applicants; 