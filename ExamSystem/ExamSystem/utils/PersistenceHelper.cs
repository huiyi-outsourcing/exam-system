using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;

namespace ExamSystem.utils {

    public class PersistenceHelper {

        #region Declarations
        // Member variables
        private static ISessionFactory sessionFactory = new Configuration().Configure("../conf/hibernate.cfg.xml").BuildSessionFactory();
        #endregion

        #region Constructor
        public PersistenceHelper() { }
        #endregion

        #region Public Methods
        /// <summary>
        /// Deletes an object of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public static void Delete<T>(T item) {
            using (ISession session = sessionFactory.OpenSession()) {
                using (session.BeginTransaction()) {
                    session.Delete(item);
                    session.Transaction.Commit();
                }
            }
        }

        /// <summary>
        /// Deletes objects of a specified type.
        /// </summary>
        /// <param name="itemsToDelete">The items to delete.</param>
        /// <typeparam name="T">The type of objects to delete.</typeparam>
        public static void Delete<T>(IList<T> itemsToDelete) {
            using (ISession session = sessionFactory.OpenSession()) {
                foreach (T item in itemsToDelete) {
                    using (session.BeginTransaction()) {
                        session.Delete(item);
                        session.Transaction.Commit();
                    }
                }
            }
        }

        /// <summary>
        /// Retrieves all objects of a given type.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <returns>A list of all objects of the specified type.</returns>
        public static IList<T> RetrieveAll<T>() {
            using (ISession session = sessionFactory.OpenSession()) {
                ICriteria criteria = session.CreateCriteria(typeof(T));
                IList<T> itemList = criteria.List<T>();

                return itemList;
            }
        }

        /// <summary>
        /// Retrieves objects of a specified type where a specified property equals a specified value.
        /// </summary>
        /// <typeparam name="T">The type of the objects to be retrieved.</typeparam>
        /// <param name="propertyName">The name of the property to be tested.</param>
        /// <param name="propertyValue">The value that the named property must hold.</param>
        /// <returns>A list of all objects meeting the specified criteria.</returns>
        public static IList<T> RetrieveByProperty<T>(string propertyName, object propertyValue) {
            using (ISession session = sessionFactory.OpenSession()) {
                // Create a criteria object with the specified criteria
                ICriteria criteria = session.CreateCriteria(typeof(T));
                criteria.Add(Expression.Eq(propertyName, propertyValue));

                // get matching objects
                IList<T> machingObjects = criteria.List<T>();

                return machingObjects; 
            }
        }

        /// <summary>
        /// Saves an object and its persistent children.
        /// </summary>
        public static void Save<T>(T item) {
            using (ISession session = sessionFactory.OpenSession()) {
                using (session.BeginTransaction()) {
                    session.SaveOrUpdate(item);
                    session.Transaction.Commit();
                }
            }
        }
        #endregion
    }
}
