using insurance_management.Utilities;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using insurance_management.dao;
using insurance_management.Models;
using insurance_management.Exceptions;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace insurance_management.dao
{
    public class InsuranceServiceImpl : IPolicyService
    {
        public bool CreatePolicy(Policy policy)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                // Adjust SQL to include premiumAmount
                string query = "INSERT INTO Policies (policyName, policyDetails, premiumAmount) VALUES (@PolicyName, @PolicyDetails, @PremiumAmount,@ClientId)";
                using (var command = new SqlCommand(query, connection))
                {
                    // Add parameters including premiumAmount
                    command.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                    command.Parameters.AddWithValue("@PolicyDetails", policy.PolicyDetails);
                    command.Parameters.AddWithValue("@PremiumAmount", policy.PremiumAmount);
                    command.Parameters.AddWithValue("@ClientId", policy.ClientId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the policy was created successfully
                }
            }
        }

        public Policy GetPolicy(int policyId)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Policies WHERE PolicyId = @PolicyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PolicyId", policyId);

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Policy
                            {
                                PolicyId = reader.GetInt32(reader.GetOrdinal("PolicyId")),
                                PolicyName = reader.GetString(reader.GetOrdinal("PolicyName")),
                                PolicyDetails = reader.GetString(reader.GetOrdinal("PolicyDetails"))
                                // Map other properties as needed
                            };
                        }
                        else
                        {
                            return null; // Policy not found
                        }
                    }
                }
            }
        }

        public IEnumerable<Policy> GetAllPolicies()
        {
            var policies = new List<Policy>();
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Policies";
                using (var command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var policy = new Policy
                            {
                                PolicyId = reader.GetInt32(reader.GetOrdinal("PolicyId")),
                                PolicyName = reader.GetString(reader.GetOrdinal("PolicyName")),
                                PolicyDetails = reader.GetString(reader.GetOrdinal("PolicyDetails"))
                                // Map other properties as needed
                            };
                            policies.Add(policy);
                        }
                    }
                }
            }
            return policies;
        }

        public bool UpdatePolicy(Policy policy)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "UPDATE Policies SET PolicyName = @PolicyName, PolicyDetails = @PolicyDetails WHERE PolicyId = @PolicyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PolicyName", policy.PolicyName);
                    command.Parameters.AddWithValue("@PolicyDetails", policy.PolicyDetails);
                    command.Parameters.AddWithValue("@PolicyId", policy.PolicyId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the policy was updated successfully
                }
            }
        }

        public bool DeletePolicy(int policyId)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "DELETE FROM Policies WHERE PolicyId = @PolicyId";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@PolicyId", policyId);

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the policy was deleted successfully
                }
            }
        }
        public User Login(string username, string password)
        {
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "SELECT * FROM Users WHERE username = @Username AND password = @Password";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password); // In a production scenario, ensure this is hashed

                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new User
                            {
                                UserId = reader.GetInt32(reader.GetOrdinal("userId")),
                                Username = reader.GetString(reader.GetOrdinal("username")),
                                Role = reader.GetString(reader.GetOrdinal("role"))
                            };
                        }
                        else
                        {
                            return null; // Invalid credentials
                        }
                    }
                }
            }
        }

        public bool ClaimPolicy(int policyId, int clientId, string claimDetails)
        {
            // Step 1: Get the policy details to check the clientId
            var policy = GetPolicy(policyId);
            if (policy == null)
            {
                throw new PolicyNotFoundException($"Policy with ID {policyId} not found.");
            }

            // Step 2: Check if the clientId matches the one associated with the policy
            if (policy.ClientId != clientId)
            {
                throw new InvalidOperationException("Client ID does not match the policy's client.");
            }

            // Step 3: Proceed to insert the claim
            using (var connection = DBConnUtil.GetConnection())
            {
                string query = "INSERT INTO Claims (ClaimNumber, DateFiled, ClaimAmount, Status, PolicyId, ClientId) VALUES (@ClaimNumber, @DateFiled, @ClaimAmount, @Status, @PolicyId, @ClientId)";

                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ClaimNumber", Guid.NewGuid().ToString()); // Generate a unique claim number
                    command.Parameters.AddWithValue("@DateFiled", DateTime.Now); // Current date
                    command.Parameters.AddWithValue("@ClaimAmount", 0); // Initial claim amount, update as needed
                    command.Parameters.AddWithValue("@Status", "Pending"); // Initial status
                    command.Parameters.AddWithValue("@PolicyId", policyId); // Policy ID
                    command.Parameters.AddWithValue("@ClientId", clientId); // Client ID

                    connection.Open();
                    int result = command.ExecuteNonQuery();
                    return result > 0; // Return true if the claim was created successfully
                }
            }
        }
    }
}


           