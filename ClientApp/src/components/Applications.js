import React, { useState, useEffect } from 'react';
import { getApplications, createApplication, updateApplicationState, deleteApplication, getApplicants, getBootcamps } from '../services/api';

function Applications() {
  const [applications, setApplications] = useState([]);
  const [applicants, setApplicants] = useState([]);
  const [bootcamps, setBootcamps] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showForm, setShowForm] = useState(false);
  const [formData, setFormData] = useState({
    applicantId: '',
    bootcampId: ''
  });

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try {
      const [applicationsRes, applicantsRes, bootcampsRes] = await Promise.all([
        getApplications(),
        getApplicants(),
        getBootcamps()
      ]);
      setApplications(applicationsRes.data);
      setApplicants(applicantsRes.data);
      setBootcamps(bootcampsRes.data);
    } catch (error) {
      console.error('Error loading data:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await createApplication(formData);
      setFormData({ applicantId: '', bootcampId: '' });
      setShowForm(false);
      loadData();
    } catch (error) {
      alert('Error creating application: ' + error.message);
    }
  };

  const handleStateChange = async (id, newState) => {
    try {
      await updateApplicationState(id, newState);
      loadData();
    } catch (error) {
      alert('Error updating application state: ' + error.message);
    }
  };

  const handleDelete = async (id) => {
    if (window.confirm('Are you sure you want to delete this application?')) {
      try {
        await deleteApplication(id);
        loadData();
      } catch (error) {
        alert('Error deleting application: ' + error.message);
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
          <h2>Applications</h2>
          <button className="btn" onClick={() => setShowForm(!showForm)}>
            {showForm ? 'Cancel' : 'Add Application'}
          </button>
        </div>

        {showForm && (
          <form onSubmit={handleSubmit} style={{ marginTop: '20px' }}>
            <div className="form-group">
              <label>Applicant:</label>
              <select
                value={formData.applicantId}
                onChange={(e) => setFormData({ ...formData, applicantId: e.target.value })}
                required
              >
                <option value="">Select Applicant</option>
                {applicants.map(applicant => (
                  <option key={applicant.id} value={applicant.id}>
                    {applicant.firstName} {applicant.lastName}
                  </option>
                ))}
              </select>
            </div>
            <div className="form-group">
              <label>Bootcamp:</label>
              <select
                value={formData.bootcampId}
                onChange={(e) => setFormData({ ...formData, bootcampId: e.target.value })}
                required
              >
                <option value="">Select Bootcamp</option>
                {bootcamps.map(bootcamp => (
                  <option key={bootcamp.id} value={bootcamp.id}>
                    {bootcamp.name}
                  </option>
                ))}
              </select>
            </div>
            <button type="submit" className="btn btn-success">Create Application</button>
          </form>
        )}

        <table className="table">
          <thead>
            <tr>
              <th>Applicant</th>
              <th>Bootcamp</th>
              <th>State</th>
              <th>Created Date</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {applications.map(application => (
              <tr key={application.id}>
                <td>{application.applicantName}</td>
                <td>{application.bootcampName}</td>
                <td>
                  <span className={`status-badge status-${application.applicationState.toLowerCase()}`}>
                    {application.applicationState}
                  </span>
                </td>
                <td>{new Date(application.createdDate).toLocaleDateString()}</td>
                <td>
                  <select
                    value={application.applicationState}
                    onChange={(e) => handleStateChange(application.id, e.target.value)}
                    style={{ marginRight: '10px' }}
                  >
                    <option value="PENDING">Pending</option>
                    <option value="IN_REVIEW">In Review</option>
                    <option value="APPROVED">Approved</option>
                    <option value="REJECTED">Rejected</option>
                    <option value="CANCELLED">Cancelled</option>
                  </select>
                  <button 
                    className="btn btn-danger" 
                    onClick={() => handleDelete(application.id)}
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

export default Applications; 