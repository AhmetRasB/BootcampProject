import React, { useState, useEffect } from 'react';
import { getBootcamps, getApplications, getApplicants, getInstructors, seedData } from '../services/api';

function Dashboard() {
  const [stats, setStats] = useState({
    bootcamps: 0,
    applications: 0,
    applicants: 0,
    instructors: 0
  });
  const [loading, setLoading] = useState(true);
  const [seeding, setSeeding] = useState(false);

  useEffect(() => {
    loadStats();
  }, []);

  const loadStats = async () => {
    try {
      const [bootcamps, applications, applicants, instructors] = await Promise.all([
        getBootcamps(),
        getApplications(),
        getApplicants(),
        getInstructors()
      ]);

      setStats({
        bootcamps: bootcamps.data.length,
        applications: applications.data.length,
        applicants: applicants.data.length,
        instructors: instructors.data.length
      });
    } catch (error) {
      console.error('Error loading stats:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleSeedData = async () => {
    setSeeding(true);
    try {
      await seedData();
      alert('Test data seeded successfully!');
      loadStats();
    } catch (error) {
      alert('Error seeding data: ' + error.message);
    } finally {
      setSeeding(false);
    }
  };

  if (loading) {
    return <div className="card">Loading...</div>;
  }

  return (
    <div>
      <div className="card">
        <h2>Dashboard</h2>
        <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '20px' }}>
          <div style={{ textAlign: 'center', padding: '20px', backgroundColor: '#3498db', color: 'white', borderRadius: '8px' }}>
            <h3>{stats.bootcamps}</h3>
            <p>Bootcamps</p>
          </div>
          <div style={{ textAlign: 'center', padding: '20px', backgroundColor: '#27ae60', color: 'white', borderRadius: '8px' }}>
            <h3>{stats.applications}</h3>
            <p>Applications</p>
          </div>
          <div style={{ textAlign: 'center', padding: '20px', backgroundColor: '#e74c3c', color: 'white', borderRadius: '8px' }}>
            <h3>{stats.applicants}</h3>
            <p>Applicants</p>
          </div>
          <div style={{ textAlign: 'center', padding: '20px', backgroundColor: '#f39c12', color: 'white', borderRadius: '8px' }}>
            <h3>{stats.instructors}</h3>
            <p>Instructors</p>
          </div>
        </div>
      </div>

      <div className="card">
        <h3>Quick Actions</h3>
        <button 
          className="btn btn-success" 
          onClick={handleSeedData}
          disabled={seeding}
        >
          {seeding ? 'Seeding...' : 'Seed Test Data'}
        </button>
      </div>
    </div>
  );
}

export default Dashboard; 