import React, { useState, useEffect } from 'react';
import { getBootcamps, createBootcamp, deleteBootcamp, getInstructors } from '../services/api';

function Bootcamps() {
  const [bootcamps, setBootcamps] = useState([]);
  const [instructors, setInstructors] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    name: '',
    instructorId: '',
    startDate: '',
    endDate: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [bootcampsRes, instructorsRes] = await Promise.all([
        getBootcamps(),
        getInstructors()
      ]);
      setBootcamps(bootcampsRes.data);
      setInstructors(instructorsRes.data);
    } catch (error) {
      console.error('Error loading data:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createBootcamp(formData);
      setFormData({ name: '', instructorId: '', startDate: '', endDate: '' });
      setShowForm(false);
      loadData();
    } catch (error) {
      alert('Error creating bootcamp: ' + error.message);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this bootcamp?')) {
      try {
        await deleteBootcamp(id);
        loadData();
      } catch (error) {
        alert('Error deleting bootcamp: ' + error.message);
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
          <h2>Bootcamps</h2>
          <button className="btn" onClick={() => setShowForm(!showForm)}>
            {showForm ? 'Cancel' : 'Add Bootcamp'}
          </button>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} style={{ marginTop: '20px' }}>
            <div className="form-group">
              <label>Name:</label>
              <input
                type="text"
                value={formData.name}
                onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>Instructor:</label>
              <select
                value={formData.instructorId}
                onChange={(e) => setFormData({ ...formData, instructorId: e.target.value })}
                required
              >
                <option value="">Select Instructor</option>
                {instructors.map(instructor => (
                  <option key={instructor.id} value={instructor.id}>
                    {instructor.firstName} {instructor.lastName}
                  </option>
                ))}
              </select>
            </div>
            <div className="form-group">
              <label>Start Date:</label>
              <input
                type="date"
                value={formData.startDate}
                onChange={(e) => setFormData({ ...formData, startDate: e.target.value })}
                required
              />
            </div>
            <div className="form-group">
              <label>End Date:</label>
              <input
                type="date"
                value={formData.endDate}
                onChange={(e) => setFormData({ ...formData, endDate: e.target.value })}
                required
              />
            </div>
            <button type="submit" className="btn btn-success">Create Bootcamp</button>
          </form>
        )}

        <table className="table">
          <thead>
            <tr>
              <th>Name</th>
              <th>Instructor</th>
              <th>Start Date</th>
              <th>End Date</th>
              <th>State</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {bootcamps.map(bootcamp => (
              <tr key={bootcamp.id}>
                <td>{bootcamp.name}</td>
                <td>{bootcamp.instructorName}</td>
                <td>{new Date(bootcamp.startDate).toLocaleDateString()}</td>
                <td>{new Date(bootcamp.endDate).toLocaleDateString()}</td>
                <td>
                  <span className={`status-badge status-${bootcamp.bootcampState.toLowerCase()}`}>
                    {bootcamp.bootcampState}
                  </span>
                </td>
                <td>
                  <button 
                    className="btn btn-danger" 
                    onClick={() => handleDelete(bootcamp.id)}
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

export default Bootcamps; 