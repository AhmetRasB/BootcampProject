import React, { useState, useEffect } from 'react';
import { getInstructors, createInstructor, deleteInstructor } from '../services/api';

function Instructors() {
  const [instructors, setInstructors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    firstName: '',
    lastName: '',
    dateOfBirth: '',
    nationalityIdentity: '',
    email: '',
    password: '',
    companyName: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const response = await getInstructors();
      setInstructors(response.data);
    } catch (error) {
      console.error('Error loading instructors:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createInstructor(formData);
      setFormData({
        firstName: '',
        lastName: '',
        dateOfBirth: '',
        nationalityIdentity: '',
        email: '',
        password: '',
        companyName: ''
      });
      setShowForm(false);
      loadData();
    } catch (error) {
      alert('Error creating instructor: ' + error.message);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this instructor?')) {
      try {
        await deleteInstructor(id);
        loadData();
      } catch (error) {
        alert('Error deleting instructor: ' + error.message);
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
          <h2>Instructors</h2>
          <button className="btn" onClick={() => setShowForm(!showForm)}>
            {showForm ? 'Cancel' : 'Add Instructor'}
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
              <label>Company Name:</label>
              <input
                type="text"
                value={formData.companyName}
                onChange={(e) => setFormData({ ...formData, companyName: e.target.value })}
                required
              />
            </div>
            <button type="submit" className="btn btn-success">Create Instructor</button>
          </form>
        )}

        <table className="table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Email</th>
              <th>Company</th>
              <th>Date of Birth</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {instructors.map(instructor => (
              <tr key={instructor.id}>
                <td>{instructor.firstName} {instructor.lastName}</td>
                <td>{instructor.email}</td>
                <td>{instructor.companyName}</td>
                <td>{new Date(instructor.dateOfBirth).toLocaleDateString()}</td>
                <td>
                  <button 
                    className="btn btn-danger" 
                    onClick={() => handleDelete(instructor.id)}
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

export default Instructors; 