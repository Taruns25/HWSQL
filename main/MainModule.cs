using insurance_management.dao;
using insurance_management.Models;
using insurance_management.Exceptions;
using System;

namespace insurance_management.main
{
    public class MainModule
    {
        public void Run()
        {
            var policyService = new InsuranceServiceImpl();
            User loggedInUser = null;

            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("Insurance Management System");
                if (loggedInUser == null)
                {
                    Console.WriteLine("1. User Login");
                    Console.WriteLine("2. Exit");
                }
                else
                {
                    Console.WriteLine("1. Create Policy");
                    Console.WriteLine("2. Get Policy");
                    Console.WriteLine("3. Get All Policies");
                    Console.WriteLine("4. Update Policy");
                    Console.WriteLine("5. Delete Policy");
                    Console.WriteLine("6. Claim for a Policy");
                    Console.WriteLine("7. Logout");
                }

                Console.Write("Enter choice: ");
                int choice = Convert.ToInt32(Console.ReadLine());

                try
                {
                    if (loggedInUser == null)
                    {
                        switch (choice)
                        {
                            case 1:
                                Console.Write("Enter username: ");
                                string username = Console.ReadLine();
                                Console.Write("Enter password: ");
                                string password = Console.ReadLine();
                                loggedInUser = policyService.Login(username, password);

                                if (loggedInUser != null)
                                {
                                    Console.WriteLine($"Welcome, {loggedInUser.Username}! You are logged in as {loggedInUser.Role}.");
                                }
                                else
                                {
                                    Console.WriteLine("Invalid username or password.");
                                }
                                break;

                            case 2:
                                exit = true; // Exit
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Please try again.");
                                break;
                        }
                    }
                    else
                    {
                        switch (choice)
                        {
                            case 1: // Create Policy
                                Console.Write("Enter policy name: ");
                                string policyName = Console.ReadLine();

                                Console.Write("Enter policy details: "); // Prompt for policy details
                                string policyDetails = Console.ReadLine();

                                Console.Write("Enter premium amount: "); // Prompt for premium amount
                                decimal premiumAmount;
                                while (!decimal.TryParse(Console.ReadLine(), out premiumAmount)) 
                                {
                                    Console.Write("Invalid input. Please enter a valid premium amount: ");
                                }

                                Console.Write("Enter client ID: "); 
                                int clientId;
                                while (!int.TryParse(Console.ReadLine(), out clientId))
                                {
                                    Console.Write("Invalid input. Please enter a valid Client ID: ");
                                }

                                // Create a policy object with all required fields
                                var newPolicy = new Policy
                                {
                                    PolicyName = policyName,
                                    PolicyDetails = policyDetails,
                                    PremiumAmount = premiumAmount,
                                    ClientId = clientId 
                                };

                                bool isCreated = policyService.CreatePolicy(newPolicy); 
                                if (isCreated)
                                {
                                    Console.WriteLine("Policy created successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to create the policy.");
                                }
                                break;



                            case 2:
                                Console.Write("Enter policy ID: ");
                                int policyId = Convert.ToInt32(Console.ReadLine());
                                var policy = policyService.GetPolicy(policyId);
                                if (policy == null)
                                {
                                    throw new PolicyNotFoundException("Policy not found");
                                }
                                Console.WriteLine($"Policy: {policy}");
                                break;

                            case 3:
                                var policies = policyService.GetAllPolicies();
                                foreach (var p in policies)
                                {
                                    Console.WriteLine(p);
                                }
                                break;

                            case 4:
                                Console.Write("Enter policy ID to update: ");
                                int updatePolicyId = Convert.ToInt32(Console.ReadLine());
                                var existingPolicy = policyService.GetPolicy(updatePolicyId);
                                if (existingPolicy == null)
                                {
                                    throw new PolicyNotFoundException($"Policy with ID {updatePolicyId} not found.");
                                }
                                Console.Write($"Current policy name: {existingPolicy.PolicyName}. Enter new name (or press Enter to keep it unchanged): ");
                                string newPolicyName = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(newPolicyName))
                                {
                                    existingPolicy.PolicyName = newPolicyName;
                                }
                                bool isUpdated = policyService.UpdatePolicy(existingPolicy);
                                if (isUpdated)
                                {
                                    Console.WriteLine("Policy updated successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to update the policy.");
                                }
                                break;

                            case 5:
                                Console.Write("Enter policy ID to delete: ");
                                int delPolicyId = Convert.ToInt32(Console.ReadLine());
                                var policyToDelete = policyService.GetPolicy(delPolicyId);
                                if (policyToDelete == null)
                                {
                                    throw new PolicyNotFoundException($"Policy with ID {delPolicyId} not found.");
                                }
                                policyService.DeletePolicy(delPolicyId);
                                Console.WriteLine("Policy deleted successfully!");
                                break;

                            case 6:
                                Console.Write("Enter policy ID to claim: ");
                                int claimPolicyId = Convert.ToInt32(Console.ReadLine());
                                Console.Write("Enter claim details: ");
                                string claimDetails = Console.ReadLine();
                                bool isClaimed = policyService.ClaimPolicy(claimPolicyId, loggedInUser.UserId, claimDetails);
                                if (isClaimed)
                                {
                                    Console.WriteLine("Claim submitted successfully!");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to submit the claim.");
                                }
                                break;

                            case 7:
                                loggedInUser = null; // Logout
                                Console.WriteLine("You have logged out.");
                                break;

                            default:
                                Console.WriteLine("Invalid choice.");
                                break;
                        }
                    }
                }
                catch (PolicyNotFoundException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a numeric value.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }
}
